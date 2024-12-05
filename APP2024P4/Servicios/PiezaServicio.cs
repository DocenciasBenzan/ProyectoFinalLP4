using APP2024P4.Data.Entities;
using APP2024P4.Data.Request;
using APP2024P4.Data.Response;
using APP2024P4.Data;
using Microsoft.EntityFrameworkCore;

namespace APP2024P4.Servicios;

public interface IPiezaServicio
{
	Task<Result> ActualizarPieza(int id, PiezaRequest request);
	Task<Result> CrearPieza(PiezaRequest request);
	Task<Result> EliminarPieza(int id);
	Task<ResultList<PiezaResponse>> ObtenerTodasLasPiezas();
}
/// <summary>
/// Servicio para la gesiton de las piezas
/// </summary>
public class PiezaServicio : IPiezaServicio
{
	private readonly ApplicationDbContext _dbContext;

	public PiezaServicio(ApplicationDbContext dbContext)
	{
		_dbContext = dbContext;
	}
	/// <summary>
	/// Obtiene todas las piezas existentes en la base de datos
	/// </summary>
	/// <returns></returns>
	public async Task<ResultList<PiezaResponse>> ObtenerTodasLasPiezas()
	{
		try
		{
			var piezas = await _dbContext.Piezas.ToListAsync();
			var response = piezas.Select(p => p.ToResponse()).ToList();
			return ResultList<PiezaResponse>.Success(response);
		}
		catch (Exception ex)
		{
			return ResultList<PiezaResponse>.Failure($"Error al obtener piezas: {ex.Message}");
		}
	}
	/// <summary>
	/// Permite registrar piezas  en la base de datos
	/// </summary>
	/// <param name="request"></param>
	/// <returns></returns>
	public async Task<Result> CrearPieza(PiezaRequest request)
	{
		try
		{
			var nuevaPieza = new Pieza
			{
				Nombre = request.Nombre,
				Precio = request.Precio,
				Imagen = request.Imagen,
				Marca = request.Marca,
				CantidadDisponible = request.CantidadDisponible
			};

			_dbContext.Piezas.Add(nuevaPieza);
			await _dbContext.SaveChangesAsync();
			return Result.Success("Pieza creada exitosamente.");
		}
		catch (Exception ex)
		{
			return Result.Failure($"Error al crear la pieza: {ex.Message}");
		}
	}
	/// <summary>
	///		Permite actualizar una pieza existente en la base de datos
	/// </summary>
	/// <param name="id"></param>
	/// <param name="request"></param>
	/// <returns></returns>
	public async Task<Result> ActualizarPieza(int id, PiezaRequest request)
	{
		try
		{
			var pieza = await _dbContext.Piezas.FindAsync(id);
			if (pieza == null)
				return Result.Failure($"No se encontró la pieza con ID {id}.");

			if (pieza.Actualizar(request))
			{
				_dbContext.Piezas.Update(pieza);
				await _dbContext.SaveChangesAsync();
				return Result.Success("Pieza actualizada exitosamente.");
			}
			return Result.Failure("Sin Cambios Realizados");
		}
		catch (Exception ex)
		{
			return Result.Failure($"Error al actualizar la pieza: {ex.Message}");
		}
	}
	/// <summary>
	/// Elimina una pieza de la base de datos
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	public async Task<Result> EliminarPieza(int id)
	{
		try
		{
			var pieza = await _dbContext.Piezas.FindAsync(id);
			if (pieza == null)
				return Result.Failure($"No se encontró la pieza con ID {id}.");

			_dbContext.Piezas.Remove(pieza);
			await _dbContext.SaveChangesAsync();
			return Result.Success("Pieza eliminada exitosamente.");
		}
		catch (Exception ex)
		{
			return Result.Failure($"Error al eliminar la pieza: {ex.Message}");
		}
	}
}
