using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProjectBlazor.Data;
using ProjectBlazor.Dto;
using ProjectBlazor.Entities;

namespace ProjectBlazor.Services
{
    public interface IClienteService
    {
        Task<Result> Create(ClienteRequest cliente);
        Task<Result> Delete(int ClienteId);
        Task<ResultList<ClienteDto>> Get(string filtro = "");
        Task<Result<ClienteDto>> GetById(int ClienteId);
        Task<Result> Update(ClienteRequest cliente);
    }

    public partial class ClienteService(IApplicationDbContext dbContext) : IClienteService
    {
        public readonly IApplicationDbContext dbContext = dbContext;


        public async Task<Result> Create(ClienteRequest cliente)
        {
            try
            {
                var entity = Cliente.Create(cliente.Nombre, cliente.Apellido, cliente.NumeroTelefonico, cliente.Cedula, cliente.Direccion);
                dbContext.Clientes.Add(entity);
                await dbContext.SaveChangesAsync();
                return Result.Success("✅ Cliente registrado con éxito!");
            }
            catch (Exception Ex)
            {
                return Result.Failure($"☠️ Error: {Ex.Message}");
            }
        }
        public async Task<Result> Update(ClienteRequest cliente)
        {
            try
            {
                var entity = dbContext.Clientes.Where(c => c.ClienteId == cliente.ClienteId).FirstOrDefault();
                if (entity == null)
                    return Result.Failure($"El cliente '{cliente.ClienteId}' no existe!");

                if (entity.Update(cliente.Nombre, cliente.Apellido, cliente.NumeroTelefonico, cliente.Cedula, cliente.Direccion))
                {
                    await dbContext.SaveChangesAsync();
                    return Result.Success("✅ Cliente modificado con éxito!");
                }

                return Result.Success("🐫 No has realizado ningún cambio!");
            }
            catch (Exception Ex)
            {
                return Result.Failure($"☠️ Error: {Ex.Message}");
            }
        }

        public async Task<Result<ClienteDto>> GetById(int ClienteId)
        {
            try
            {
                var entity = await dbContext.Clientes.Where(v => v.ClienteId == ClienteId)
                    .Select(v => new ClienteDto(v.ClienteId, v.Nombre, v.Apellido, v.NumeroTelefonico, v.Cedula, v.Direccion))
                    .FirstOrDefaultAsync();

                if (entity == null)
                    return Result<ClienteDto>.Failure($"El cliente '{ClienteId}' no existe!");

                return Result<ClienteDto>.Success(entity);
            }
            catch (Exception Ex)
            {
                return Result<ClienteDto>.Failure($"☠️ Error: {Ex.Message}");
            }
        }

        public async Task<ResultList<ClienteDto>> Get(string filtro = "")
        {
            try
            {
                var entities = await dbContext.Clientes
                    .Where(c => c.Nombre.ToLower().Contains(filtro.ToLower()))
                    .Select(c => new ClienteDto(c.ClienteId, c.Nombre, c.Apellido, c.NumeroTelefonico, c.Cedula, c.Direccion))
                    .ToListAsync();

                return ResultList<ClienteDto>.Success(entities);
            }
            catch (Exception Ex)
            {
                return ResultList<ClienteDto>.Failure($"☠️ Error: {Ex.Message}");
            }
        }

        public async Task<ResultList<ClienteDto>> Get(string filtro = "")
        {
            try
            {
                var entities = await dbContext.Clientes
                    .Where(c => c.Nombre.ToLower().Contains(filtro.ToLower()))
                    .Select(c => new ClienteDto(c.ClienteId, c.Nombre, c.Apellido, c.NumeroTelefonico, c.Cedula, c.Direccion))
                    .ToListAsync();

                return ResultList<ClienteDto>.Success(entities);
            }
            catch (Exception Ex)
            {
                return ResultList<ClienteDto>.Failure($"☠️ Error: {Ex.Message}");
            }
        }


    }
}
