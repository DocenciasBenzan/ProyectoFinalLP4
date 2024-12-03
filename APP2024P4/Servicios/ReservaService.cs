using APP2024P4.Data.Entities;
using APP2024P4.Data;
using Microsoft.EntityFrameworkCore;
using APP2024P4.Data.Request;
using APP2024P4.Data.Response;

namespace APP2024P4.Servicios;

public interface IReservaService
{
	Task<Result> ActualizarReservaAsync(ReservaRequest request);
	Task<Result> CrearReservaAsync(ReservaRequest request);
	Task<Result> EliminarReservaAsync(int id);
	Task<Result<ReservaResponse>> ObtenerReservaPorIdAsync(int id);
	Task<ResultList<ReservaResponse>> ObtenerTodasLasReservasAsync();
}

public class ReservaService(ApplicationDbContext context) : IReservaService
{
	public async Task<ResultList<ReservaResponse>> ObtenerTodasLasReservasAsync()
	{
		try
		{
			var reservas = await context.Reservas.AsNoTracking().Select(x=>x.ToResponse()).ToListAsync();
			return ResultList<ReservaResponse>.Success(reservas);
		}
		catch (Exception ex)
		{
			return ResultList<ReservaResponse>.Failure($"Error al obtener reservas: {ex.Message}");
		}
	}

	public async Task<Result<ReservaResponse>> ObtenerReservaPorIdAsync(int id)
	{
		try
		{
			var reserva = await context.Reservas.FindAsync(id);
			if (reserva != null)
			{
				return Result<ReservaResponse>.Success(reserva.ToResponse());
			}
			else
			{
				return Result<ReservaResponse>.Failure("Reserva no encontrada");
			}
		}
		catch (Exception ex)
		{
			return Result<ReservaResponse>.Failure($"Error al obtener reserva: {ex.Message}");
		}
	}

	public async Task<Result> CrearReservaAsync(ReservaRequest request)
	{
		try
		{
			context.Reservas.Add(request.ToReserva());
			await context.SaveChangesAsync();
			return Result.Success();
		}
		catch (Exception ex)
		{
			return Result.Failure($"Error al crear reserva: {ex.Message}");
		}
	}

	public async Task<Result> ActualizarReservaAsync(ReservaRequest request)
	{
		try
		{
			var reserva = context.Reservas.FirstOrDefault(x => x.Id == request.Id);
			if (reserva == null)
			{
				return Result.Failure("Reserva no encontrada");
			}
			if (reserva.Actualizar(request))
			{
				await context.SaveChangesAsync();
				return Result.Success("Actualizacion realizada");
			}
			return Result.Failure("Sin cambios realizados");

		}
		catch (Exception ex)
		{
			return Result.Failure($"Error al actualizar reserva: {ex.Message}");
		}
	}

	public async Task<Result> EliminarReservaAsync(int id)
	{
		try
		{
			var reserva = await context.Reservas.FirstOrDefaultAsync(x => x.Id == id);
			if (reserva != null)
			{
				context.Reservas.Remove(reserva);
				await context.SaveChangesAsync();
				return Result.Success();
			}
			else
			{
				return Result.Failure("Reserva no encontrada");
			}
		}
		catch (Exception ex)
		{
			return Result.Failure($"Error al eliminar reserva: {ex.Message}");
		}
	}
}