using APP2024P4.Data.Entities;
using APP2024P4.Data.Request;
using APP2024P4.Data.Response;
using APP2024P4.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
namespace APP2024P4.Servicios;

public interface IFacturaServicio
{
	Task<Result> CrearFactura(FacturaRequest request);
	Task<Result> EliminarFactura(int id);
	Task<Result> GuardarItemFactura(List<FacturaParteRequest> request);
	Result<FacturaResponse> ObtenerFacturaPorId(int id);
	Task<ResultList<FacturaResponse>> ObtenerTodasLasFacturas();
}

public class FacturaServicio : IFacturaServicio
{
	private readonly ApplicationDbContext _dbContext;
	private readonly IPiezaServicio piezaServicio;

	public FacturaServicio(ApplicationDbContext dbContext, IPiezaServicio piezaServicio)
	{
		_dbContext = dbContext;
		this.piezaServicio = piezaServicio;
	}

	/// <summary>
	/// Permite obtner todas las facturas existentes en la base de datos
	/// </summary>
	/// <returns></returns>
	public async Task<ResultList<FacturaResponse>> ObtenerTodasLasFacturas()
	{
		try
		{
			var facturas = _dbContext.Facturas
					.Include(c => c.Cliente)
					.Include(x => x.FacturaPartes)
						.ThenInclude(x => x.Pieza)
					.ToList();

			var data = facturas.Select(x => new FacturaResponse()
			{
				FacturaID = x.FacturaID,
				Fecha = x.Fecha,
				Total = x.Total, // ¿cambiar a parte de la factura?
				Cliente = new ClienteResponse()
				{
					Id = x.Cliente.Id,
					Nombre = x.Cliente.Nombre,
				},
				FacturaPartes = x.FacturaPartes.Select(p => new FacturaParteResponse()
				{
					Id = p.Id,
					Cantidad = p.Cantidad,
					FacturaID = x.FacturaID,
					// FACTURA
					PiezaId = p.PiezaId,
					pieza = new PiezaResponse()
					{
						Id = p.PiezaId,
						Nombre = p.Pieza.Nombre,
						Precio = p.Pieza.Precio,
						Imagen = p.Pieza.Imagen,
						Marca = p.Pieza.Marca,
						CantidadDisponible = p.Pieza.CantidadDisponible,
					}
				}).ToList()
			}).ToList();
			return ResultList<FacturaResponse>.Success(data);
		}
		catch (Exception ex)
		{
			return ResultList<FacturaResponse>.Failure($"Error al obtener facturas: {ex.Message}");
		}
	}
	/// <summary>
	/// Obtiene una factura en especifica atraves de su ID
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	public Result<FacturaResponse> ObtenerFacturaPorId(int id)
	{
		try
		{
			var factura = _dbContext.Facturas
					.Include(c => c.Cliente)
					.Include(x => x.FacturaPartes)
						.ThenInclude(x => x.Pieza)
					.FirstOrDefault();

			var data = new FacturaResponse()
			{
				FacturaID = factura.FacturaID,
				Fecha = factura.Fecha,
				Total = factura.Total, // ¿cambiar a parte de la factura?
				Cliente = new ClienteResponse()
				{
					Id = factura.Cliente.Id,
					Nombre = factura.Cliente.Nombre,
				},
				FacturaPartes = factura.FacturaPartes.Select(p => new FacturaParteResponse()
				{
					Id = p.Id,
					Cantidad = p.Cantidad,
					FacturaID = factura.FacturaID,
					// FACTURA
					PiezaId = p.PiezaId,
					pieza = new PiezaResponse()
					{
						Id = p.PiezaId,
						Nombre = p.Pieza.Nombre,
						Precio = p.Pieza.Precio,
						Imagen = p.Pieza.Imagen,
						Marca = p.Pieza.Marca,
						CantidadDisponible = p.Pieza.CantidadDisponible,
					}
				}).ToList()
			};

			return Result<FacturaResponse>.Success(data);
		}
		catch (Exception ex)
		{
			return Result<FacturaResponse>.Failure($"Error al obtener la factura: {ex.Message}");
		}
	}
	/// <summary>
	/// Permite registar facturas
	/// </summary>
	/// <param name="request"></param>
	/// <returns></returns>
	public async Task<Result> CrearFactura(FacturaRequest request)
	{
		try
		{
			var nuevaFactura = new Factura
			{
				Fecha = request.Fecha,
				Total = request.FacturaPartes.Sum(fp => fp.Cantidad * fp.Pieza.Precio),
				ClienteId = request.Cliente.Id,
			};

			_dbContext.Facturas.Add(nuevaFactura);


			await _dbContext.SaveChangesAsync();
			var id = _dbContext.Facturas.OrderBy(x => x.FacturaID).LastOrDefault()?.FacturaID ?? 0;
			Console.WriteLine($"---------- Nuevo id factura:: {id}");

			if (id != 0)
			{
				request.FacturaID = id;
				request.FacturaPartes.ForEach(x => x.FacturaID = id);
				var r = await GuardarItemFactura(request.FacturaPartes);
				if (r.Ok)
				{
					return Result.Success("Factura creada");

				}
			}
			return Result.Failure("Error al crear la factura");
		}
		catch (Exception ex)
		{
			return Result.Failure($"Error al crear la factura: {ex.Message}");
		}
	}

	/// <summary>
	/// Permite la eliminacion de facturas existentes
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	public async Task<Result> EliminarFactura(int id)
	{
		try
		{
			var factura = await _dbContext.Facturas
				.Include(f => f.FacturaPartes)
				.FirstOrDefaultAsync(f => f.FacturaID == id);

			if (factura == null)
				return Result.Failure($"No se encontró la factura con ID {id}.");
			_dbContext.FacturaPartes.RemoveRange(factura.FacturaPartes);
			_dbContext.Facturas.Remove(factura);
			await _dbContext.SaveChangesAsync();

			return Result.Success("Factura eliminada exitosamente.");
		}
		catch (Exception ex)
		{
			return Result.Failure($"Error al eliminar la factura: {ex.Message}");
		}
	}

	/// <summary>
	/// Encargado de guardar las piezas seleccionadas en su correspondiente tabla
	/// </summary>
	/// <param name="request"></param>
	/// <returns></returns>
	public async Task<Result> GuardarItemFactura(List<FacturaParteRequest> request)
	{
		try
		{
			var items = request.Select(i =>
				new FacturaParte()
				{
					FacturaID = i.FacturaID,
					PiezaId = i.PiezaId,
					Cantidad = i.Cantidad
				}
			).ToList();

			_dbContext.FacturaPartes.AddRange(items);
			await _dbContext.SaveChangesAsync();


			foreach (var p in request)
			{
				await piezaServicio.ActualizarPieza(p.PiezaId, p.Pieza);
			}
			return Result.Success("Items agregados");

		}
		catch (Exception ex)
		{
			return Result.Failure($"Error al guardar la factura: {ex.Message}");
		}
	}



}


