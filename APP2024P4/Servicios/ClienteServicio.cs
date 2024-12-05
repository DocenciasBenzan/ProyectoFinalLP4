using APP2024P4.Data.Entities;
using APP2024P4.Data.Request;
using APP2024P4.Data.Response;
using APP2024P4.Data;
using Microsoft.EntityFrameworkCore;

namespace APP2024P4.Servicios;

public interface IClienteServicio
{
	Task<Result> ActualizarCliente(int id, ClienteRequest request);
	Task<Result> CrearCliente(ClienteRequest request);
	Task<Result> EliminarCliente(int id);
	Task<ResultList<ClienteResponse>> ObtenerTodosLosClientes();
}

public class ClienteServicio : IClienteServicio
{
	private readonly ApplicationDbContext _dbContext;

	public ClienteServicio(ApplicationDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task<ResultList<ClienteResponse>> ObtenerTodosLosClientes()
	{
		try
		{
			var clientes = await _dbContext.Clientes.ToListAsync();
			var response = clientes.Select(c => new ClienteResponse
			{
				Id = c.Id,
				Nombre = c.Nombre
			}).ToList();

			return ResultList<ClienteResponse>.Success(response);
		}
		catch (Exception ex)
		{
			return ResultList<ClienteResponse>.Failure($"Error al obtener clientes: {ex.Message}");
		}
	}

	public async Task<Result> CrearCliente(ClienteRequest request)
	{
		try
		{
			var nuevoCliente = new Cliente
			{
				Nombre = request.Nombre
			};

			_dbContext.Clientes.Add(nuevoCliente);
			await _dbContext.SaveChangesAsync();
			return Result.Success("Cliente creado exitosamente.");
		}
		catch (Exception ex)
		{
			return Result.Failure($"Error al crear el cliente: {ex.Message}");
		}
	}

	public async Task<Result> ActualizarCliente(int id, ClienteRequest request)
	{
		try
		{
			var cliente = await _dbContext.Clientes.FindAsync(id);
			if (cliente == null)
				return Result.Failure($"No se encontró el cliente con ID {id}.");

			cliente.Nombre = request.Nombre;
			_dbContext.Clientes.Update(cliente);
			await _dbContext.SaveChangesAsync();
			return Result.Success("Cliente actualizado exitosamente.");
		}
		catch (Exception ex)
		{
			return Result.Failure($"Error al actualizar el cliente: {ex.Message}");
		}
	}

	public async Task<Result> EliminarCliente(int id)
	{
		try
		{
			var cliente = await _dbContext.Clientes.FindAsync(id);
			if (cliente == null)
				return Result.Failure($"No se encontró el cliente con ID {id}.");

			_dbContext.Clientes.Remove(cliente);
			await _dbContext.SaveChangesAsync();
			return Result.Success("Cliente eliminado exitosamente.");
		}
		catch (Exception ex)
		{
			return Result.Failure($"Error al eliminar el cliente: {ex.Message}");
		}
	}
}
