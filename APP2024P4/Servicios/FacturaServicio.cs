using APP2024P4.Data.Entities;
using APP2024P4.Data.Request;
using APP2024P4.Data.Response;
using APP2024P4.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
namespace APP2024P4.Servicios;

public interface IFacturaServicio
{
	Task<Result> ActualizarFactura(int id, FacturaRequest request);
	Task<Result> CrearFactura(FacturaRequest request);
	Task<Result> EliminarFactura(int id);
	Task<ResultList<FacturaResponse>> ObtenerTodasLasFacturas();
}

public class FacturaServicio : IFacturaServicio
{
	private readonly ApplicationDbContext _dbContext;
	private readonly IJSRuntime js;

	public FacturaServicio(ApplicationDbContext dbContext, IJSRuntime js)
	{
		_dbContext = dbContext;
		this.js = js;
	}

	public async Task<ResultList<FacturaResponse>> ObtenerTodasLasFacturas()
	{
		try
		{
			//var facturas = await _dbContext.Facturas
			//	.Include(f => f.FacturaPartes)
			//		.ThenInclude(fp => fp.Pieza)
			//	.Include(f => f.Cliente)
			//	.ToListAsync();
			var facturas = _dbContext.Facturas.Include(x=>x.FacturaPartes).ToList();

			if (facturas != null)
			{
				foreach (var x in facturas)
				{
					Console.WriteLine($"ID: {x.FacturaID} - partes: {x.FacturaPartes.Count}");
				}
			}
			else
			{
				Console.WriteLine("SIn facturas registradas");
			}

			await js.InvokeVoidAsync("console.log", facturas);

			//var response = facturas.Select(f => f.ToResponse()).ToList();
			//return ResultList<FacturaResponse>.Success(response);
			return ResultList<FacturaResponse>.Success(new List<FacturaResponse>());
		}
		catch (Exception ex)
		{
			return ResultList<FacturaResponse>.Failure($"Error al obtener facturas: {ex.Message}");
		}
	}


	public async Task<Result> CrearFactura(FacturaRequest request)
	{
		try
		{
			var nuevaFactura = new Factura
			{
				Fecha = request.Fecha,
				Total = request.FacturaPartes.Sum(fp => fp.Cantidad * fp.Pieza.Precio),
				ClienteId = request.Cliente.Id,
				//FacturaPartes = request.FacturaPartes.Select(fp => new FacturaParte
				//{
				//	PiezaId = fp.PiezaId,
				//	Cantidad = fp.Cantidad
				//}).ToList()
			};

			_dbContext.Facturas.Add(nuevaFactura);
			await _dbContext.SaveChangesAsync();
			return Result.Success("Factura creada exitosamente.");
		}
		catch (Exception ex)
		{
			return Result.Failure($"Error al crear la factura: {ex.Message}");
		}
	}

	public async Task<Result> ActualizarFactura(int id, FacturaRequest request)
	{
		try
		{
			var factura = await _dbContext.Facturas
				.Include(f => f.FacturaPartes)
				.FirstOrDefaultAsync(f => f.FacturaID == id);

			if (factura == null)
				return Result.Failure($"No se encontró la factura con ID {id}.");

			factura.Fecha = request.Fecha;
			factura.Total = request.FacturaPartes.Sum(fp => fp.Cantidad * fp.Pieza.Precio);
			factura.ClienteId = request.Cliente.Id;

			// Manejo de FacturaPartes
			factura.FacturaPartes.Clear();
			factura.FacturaPartes.AddRange(request.FacturaPartes.Select(fp => new FacturaParte
			{
				PiezaId = fp.PiezaId,
				Cantidad = fp.Cantidad
			}));

			_dbContext.Facturas.Update(factura);
			await _dbContext.SaveChangesAsync();
			return Result.Success("Factura actualizada exitosamente.");
		}
		catch (Exception ex)
		{
			return Result.Failure($"Error al actualizar la factura: {ex.Message}");
		}
	}

	public async Task<Result> EliminarFactura(int id)
	{
		try
		{
			var factura = await _dbContext.Facturas
				.Include(f => f.FacturaPartes)
				.FirstOrDefaultAsync(f => f.FacturaID == id);

			if (factura == null)
				return Result.Failure($"No se encontró la factura con ID {id}.");

			_dbContext.Facturas.Remove(factura);
			await _dbContext.SaveChangesAsync();
			return Result.Success("Factura eliminada exitosamente.");
		}
		catch (Exception ex)
		{
			return Result.Failure($"Error al eliminar la factura: {ex.Message}");
		}
	}
}


