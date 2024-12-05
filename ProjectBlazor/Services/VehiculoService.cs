using Microsoft.EntityFrameworkCore;
using ProjectBlazor.Data;
using ProjectBlazor.Dto;
using ProjectBlazor.Entities;

namespace ProjectBlazor.Services
{
	public interface IVehiculoService
	{
		Task<Result> Create(VehiculoRequest vehiculo);
		Task<Result> Delete(int VehiculoId);
		Task<ResultList<VehiculoDto>> Get(string filtro = "");
		Task<Result<VehiculoDto>> GetById(int VehiculoId);
		Task<Result> Update(VehiculoRequest vehiculo);
	}

	public partial class VehiculoService : IVehiculoService
	{
		public readonly IApplicationDbContext dbContext;
		public VehiculoService(IApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task<Result> Create(VehiculoRequest vehiculo)
		{
			try
			{
				var entity = Vehiculo.Create(vehiculo.Marca, vehiculo.Modelo, vehiculo.Matricula, vehiculo.NumeroPlaca, vehiculo.Tipo, vehiculo.Year, vehiculo.Estatus, vehiculo.Precio);
				dbContext.Vehiculos.Add(entity);
				await dbContext.SaveChangesAsync();
				return Result.Success("✅Vehiculo registrado con exito!");
			}
			catch (Exception Ex)
			{
				return Result.Failure($"☠️ Error: {Ex.Message}");
			}
		}
		public async Task<Result> Update(VehiculoRequest vehiculo)
		{
			try
			{
				var entity = dbContext.Vehiculos.Where(v => v.VehiculoId == vehiculo.VehiculoId).FirstOrDefault();
				if (entity == null)
					return Result.Failure($"El vehiculo '{vehiculo.VehiculoId}' no existe!");
				if (entity.Update(vehiculo.Marca, vehiculo.Modelo, vehiculo.Matricula, vehiculo.NumeroPlaca, vehiculo.Tipo, vehiculo.Year, vehiculo.Estatus, vehiculo.Precio))
				{
					await dbContext.SaveChangesAsync();
					return Result.Success("✅Vehiculo modificado con exito!");
				}
				return Result.Success("🐫 No has realizado ningun cambio!");
			}
			catch (Exception Ex)
			{
				return Result.Failure($"☠️ Error: {Ex.Message}");
			}
		}
		public async Task<Result> Delete(int VehiculoId)
		{
			try
			{
				var entity = dbContext.Vehiculos.Where(v => v.VehiculoId == VehiculoId).FirstOrDefault();
				if (entity == null)
					return Result.Failure($"El vehiculo '{VehiculoId}' no existe!");
				dbContext.Vehiculos.Remove(entity);
				await dbContext.SaveChangesAsync();
				return Result.Success("✅Vehiculo eliminado con exito!");
			}
			catch (Exception Ex)
			{
				return Result.Failure($"☠️ Error: {Ex.Message}");
			}
		}
		public async Task<Result<VehiculoDto>> GetById(int VehiculoId)
		{
			try
			{
				var entity = await dbContext.Vehiculos.Where(v => v.VehiculoId == VehiculoId)
					.Select(v => new VehiculoDto(v.VehiculoId, v.Marca, v.Modelo, v.Matricula, v.NumeroPlaca, v.Tipo, v.Year, v.Estatus, v.Precio))
					.FirstOrDefaultAsync();
				if (entity == null)
					return Result<VehiculoDto>.Failure($"El vehiculo '{VehiculoId}' no existe!");

				return Result<VehiculoDto>.Success(entity);
			}
			catch (Exception Ex)
			{
				return Result<VehiculoDto>.Failure($"☠️ Error: {Ex.Message}");
			}
		}
		public async Task<ResultList<VehiculoDto>> Get(string filtro = "")
		{
			try
			{
				var entities = await dbContext.Vehiculos
					.Where(v => v.Marca.ToLower().Contains(filtro.ToLower()))
					.Select(v => new VehiculoDto(v.VehiculoId, v.Marca, v.Modelo, v.Matricula, v.NumeroPlaca, v.Tipo, v.Year, v.Estatus, v.Precio))
					.ToListAsync();
				return ResultList<VehiculoDto>.Success(entities);
			}
			catch (Exception Ex)
			{
				return ResultList<VehiculoDto>.Failure($"☠️ Error: {Ex.Message}");
			}
		}
        
    }

}
