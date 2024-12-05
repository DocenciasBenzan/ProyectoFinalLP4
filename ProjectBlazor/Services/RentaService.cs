using Microsoft.EntityFrameworkCore;
using ProjectBlazor.Components.Pages.Clientes;
using ProjectBlazor.Data;
using ProjectBlazor.Dto;
using ProjectBlazor.Entities;

namespace ProjectBlazor.Services
{
	public interface IRentaService
	{
		Task<Result> Create(RentaRequest renta);
		Task<ResultList<RentaDto>> Get(string filtro = "");
		Task<ResultList<RentaDto>> GetAll(CancellationToken cancellationToken = default);
		Task<Result<RentaDto>> GetById(int RentaId);


    }

	public partial class RentaService : IRentaService
	{
		public readonly IApplicationDbContext dbContext;
		public RentaService(IApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task<Result> Create(RentaRequest renta)
        {
            try
            {
				var entity = Renta.Create(renta.RentaId, renta.FechaRenta, renta.FechaEntrega, renta.TotalPagado, renta.VehiculoId, renta.ClienteId);
				dbContext.Rentas.Add(entity);
				await dbContext.SaveChangesAsync();
				return Result.Success("✅Renta registrada con exito!");
			}
			catch (Exception Ex)
			{
				return Result.Failure($"☠️ Error: {Ex.Message}");
			}
		}
		
		
		
		public async Task<ResultList<RentaDto>> Get(string filtro = "")
		{
			try
			{
				var entities = await dbContext.Rentas
					.Where(v => v.RentaId.ToString().Contains(filtro.ToLower()))
					.Select(v => new RentaDto(v.RentaId, v.FechaRenta, v.FechaEntrega, v.TotalPagado, v.VehiculoId, v.ClienteId))
					.ToListAsync();
				return ResultList<RentaDto>.Success(entities);
			}
			catch (Exception Ex)
			{
				return ResultList<RentaDto>.Failure($"☠️ Error: {Ex.Message}");
			}
		}
        public async Task<ResultList<RentaDto>> GetAll(CancellationToken cancellationToken = default)
        {
            var rentas = await dbContext.Rentas
            .Select(x => x.ToDto())
            .ToListAsync(cancellationToken);
            return ResultList<RentaDto>.Success(rentas);
        }

        public async Task<Result<RentaDto>> GetById(int RentaId)
        {
            try
            {
                var entity = await dbContext.Rentas.Where(v => v.RentaId == RentaId)
                    .Select(v => new RentaDto(v.RentaId, v.FechaRenta, v.FechaEntrega, v.TotalPagado, v.VehiculoId, v.ClienteId))
                    .FirstOrDefaultAsync();

                if (entity == null)
                    return Result<RentaDto>.Failure($"El cliente '{RentaId}' no existe!");

                return Result<RentaDto>.Success(entity);
            }
            catch (Exception Ex)
            {
                return Result<RentaDto>.Failure($"☠️ Error: {Ex.Message}");
            }
        }

    }

}
