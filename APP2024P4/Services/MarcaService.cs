using APP2024P4.Data.Datos;
using APP2024P4.Data.Entities;
using APP2024P4.Data;
using Microsoft.EntityFrameworkCore;

namespace APP2024P4.Services
{
    public partial class MarcaService : IMarcaService
    {
        private readonly IApplicationDbContext dbContext;

        public MarcaService(IApplicationDbContext context)
        {
            this.dbContext = context;
        }

        //CRUD
        public async Task<Result> Create(MarcaRequest marca)
        {
            try
            {
                var entity = Marca.Create(marca.Nombre);
                dbContext.Marcas.Add(entity);
                await dbContext.SaveChangesAsync();
                return Result.Success("✅Marca registrada con exito!");
            }
            catch (Exception Ex)
            {
                return Result.Failure($"☠️ Error: {Ex.Message}");
            }
        }
        public async Task<Result> Update(MarcaRequest marca)
        {
            try
            {
                var entity = dbContext.Marcas.Where(m => m.Id == marca.Id).FirstOrDefault();
                if (entity == null)
                    return Result.Failure($"La Marca'{marca.Id}' no existe!");
                if (entity.Update(marca.Nombre))
                {
                    await dbContext.SaveChangesAsync();
                    return Result.Success("✅Marca modificada con exito!");
                }
                return Result.Success("🐫 No has realizado ningun cambio! animal");
            }
            catch (Exception Ex)
            {
                return Result.Failure($"☠️ Error: {Ex.Message}");
            }
        }
        public async Task<Result> Delete(int Id)
        {
            try
            {
                var entity = dbContext.Marcas.Where(m => m.Id == Id).FirstOrDefault();
                if (entity == null)
                    return Result.Failure($"la Marca '{Id}' no existe!");
                dbContext.Marcas.Remove(entity);
                await dbContext.SaveChangesAsync();
                return Result.Success("✅Marca eliminada con exito!");
            }
            catch (Exception Ex)
            {
                return Result.Failure($"☠️ Error: {Ex.Message}");
            }
        }
        public async Task<ResultList<MarcaDatos>> GetAll(string filtro = "")
        {
            try
            {
                var entities = await dbContext.Marcas
                    .Where(m => m.NombreMc.ToLower().Contains(filtro.ToLower()))
                    .Select(m => new MarcaDatos(m.Id, m.NombreMc))
                    .ToListAsync();
                return ResultList<MarcaDatos>.Success(entities);
            }
            catch (Exception Ex)
            {
                return ResultList<MarcaDatos>.Failure($"☠️ Error: {Ex.Message}");
            }
        }
    }
}
