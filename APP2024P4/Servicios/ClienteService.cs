using APP2024P4.Data;
using APP2024P4.Data.Request;
using APP2024P4.Data.Response;
using Microsoft.EntityFrameworkCore;

namespace APP2024P4.Servicios;

public interface IClienteService
{
	Task<Result> ActualizarCliente(ClienteRequest request);
	Task<Result> AgregarCliente(ClienteRequest request);
	Task<Result> EliminarCliente(int EmpleadoId);
	Task<ResultList<ClienteResponse>> ObtenerClientes(string filtro = "");
}

public class ClienteService(IApplicationDbContext context) : IClienteService
{
	public async Task<ResultList<ClienteResponse>> ObtenerClientes(string filtro = "")
	{
		try
		{
			var r = context.Clientes.AsNoTracking().Where(x => x.Nombre.Contains(filtro)).Select(x => x.ToResponse()).ToList();
			if (r != null)
			{
				return ResultList<ClienteResponse>.Success(r);
			}
			return ResultList<ClienteResponse>.Failure("Sin Clientes encontrados");

		}
		catch (Exception ex)
		{
			return ResultList<ClienteResponse>.Failure($"Error obteniendo los Clientes:: {ex.Message}");
		}
	}
	public async Task<Result> AgregarCliente(ClienteRequest request)
	{
		try
		{
			var Cliente = request.ToCliente();
			context.Clientes.Add(Cliente);
			await context.SaveChangesAsync().ConfigureAwait(false);
			return Result.Success();
		}
		catch (Exception ex)
		{
			return Result.Failure($"Error al agregar el cliente : {ex}");
		}
	}

	public async Task<Result> ActualizarCliente(ClienteRequest request)
	{
		try
		{
			var Cliente = context.Clientes.FirstOrDefault(x => x.Id == request.Id);
			if (Cliente == null)
			{
				return Result.Failure("Cliente no encontrado:");
			}
			if (Cliente.Actualizar(request))
			{
				await context.SaveChangesAsync().ConfigureAwait(false);
				return Result.Success();
			}
			return Result.Success("No has realizado ningun cambio!");
		}
		catch (Exception ex)
		{
			return Result.Failure($"Error al actualizar el Cliente : {ex}");
		}
	}

	public async Task<Result> EliminarCliente(int EmpleadoId)
	{
		try
		{
			var Cliente = context.Clientes.FirstOrDefault(x => x.Id == EmpleadoId);
			if (Cliente is not null)
			{
				context.Clientes.Remove(Cliente);
				await context.SaveChangesAsync();
				return Result.Success();
			}
			return Result.Failure("Cliente no encontrado");
		}
		catch (Exception ex)
		{
			return Result.Failure($"Error al Eliminar el Cliente : {ex}");
		}
	}

}
