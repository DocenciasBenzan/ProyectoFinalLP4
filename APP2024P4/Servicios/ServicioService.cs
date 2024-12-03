using APP2024P4.Data;
using APP2024P4.Data.Request;
using APP2024P4.Data.Response;
using Microsoft.EntityFrameworkCore;

namespace APP2024P4.Servicios;

public interface IServicioService
{
	Task<Result> ActualizarServicio(ServicioRequest request);
	Task<Result> AgregarServicio(ServicioRequest request);
	Task<Result> EliminarServicio(int EmpleadoId);
	Task<ResultList<ServicioResponse>> ObtenerServicios(string filtro = "");
}

public class ServicioService(IApplicationDbContext context) : IServicioService
{
	public async Task<ResultList<ServicioResponse>> ObtenerServicios(string filtro = "")
	{
		try
		{
			var r = context.Servicios.AsNoTracking().Where(x => x.Nombre.Contains(filtro)).Select(x => x.ToResponse()).ToList();
			if (r != null)
			{
				return ResultList<ServicioResponse>.Success(r);
			}
			return ResultList<ServicioResponse>.Failure("Sin servicios encontrados");

		}
		catch (Exception ex)
		{
			return ResultList<ServicioResponse>.Failure($"Error obteniendo los servicios:: {ex.Message}");
		}
	}
	public async Task<Result> AgregarServicio(ServicioRequest request)
	{
		try
		{
			var servicio = request.ToServicio();
			context.Servicios.Add(servicio);
			await context.SaveChangesAsync().ConfigureAwait(false);
			return Result.Success();
		}
		catch (Exception ex)
		{
			return Result.Failure($"Error al agregar el serivico : {ex}");
		}
	}

	public async Task<Result> ActualizarServicio(ServicioRequest request)
	{
		try
		{
			var servicio = context.Servicios.FirstOrDefault(x => x.Id == request.Id);
			if (servicio == null)
			{
				return Result.Failure("Servicio no encontrado:");
			}
			if (servicio.Actualizar(request))
			{
				await context.SaveChangesAsync().ConfigureAwait(false);
				return Result.Success();
			}
			return Result.Success("No has realizado ningun cambio!");
		}
		catch (Exception ex)
		{
			return Result.Failure($"Error al actualizar el Servicio : {ex}");
		}
	}

	public async Task<Result> EliminarServicio(int EmpleadoId)
	{
		try
		{
			var servicio = context.Servicios.FirstOrDefault(x => x.Id == EmpleadoId);
			if (servicio is not null)
			{
				context.Servicios.Remove(servicio);
				await context.SaveChangesAsync();
				return Result.Success();
			}
			return Result.Failure("Servicio no encontrado");
		}
		catch (Exception ex)
		{
			return Result.Failure($"Error al Eliminar el servicio : {ex}");
		}
	}
}
