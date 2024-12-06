using APP2024P4.Data.Datos;
using APP2024P4.Data.Entities;
using APP2024P4.Data;
using Microsoft.EntityFrameworkCore;

namespace APP2024P4.Services
{
    public partial class ModeloService : IModeloService
    {
        private readonly IApplicationDbContext dbContext;

        public ModeloService(IApplicationDbContext context)
        {
            this.dbContext = context;
        }

        //CRUD

        //Metodo para registrar
        public async Task<Result> Create(ModeloRequest modelo)
        {
            try
            {
                var entity = Modelo.Create(modelo);
                dbContext.Modelos.Add(entity);
                await dbContext.SaveChangesAsync();
                return Result.Success("✅Modelo registrada con exito!");
            }
            catch (Exception Ex)
            {
                return Result.Failure($"☠️ Error: {Ex.Message}");
            }
        }

        //Metodo para Editar
        public async Task<Result> Update(ModeloRequest modelo)
        {
            try
            {
                var entity = dbContext.Modelos.Where(m => m.Id == modelo.Id).FirstOrDefault();
                if (entity == null)
                    return Result.Failure($"El Modelo'{modelo.Id}' no existe!");
                if (entity.Update(modelo))
                {
                    await dbContext.SaveChangesAsync();
                    return Result.Success("✅Modelo modificada con exito!");
                }
                return Result.Success("🐫 No has realizado ningun cambio! animal");
            }
            catch (Exception Ex)
            {
                return Result.Failure($"☠️ Error: {Ex.Message}");
            }
        }

        //Metodo para eliminar
        public async Task<Result> Delete(int Id)
        {
            try
            {
                var entity = dbContext.Modelos.Where(m => m.Id == Id).FirstOrDefault();
                if (entity == null)
                    return Result.Failure($"El Modelo '{Id}' no existe!");
                dbContext.Modelos.Remove(entity);
                await dbContext.SaveChangesAsync();
                return Result.Success("✅Modelo eliminada con exito!");
            }
            catch (Exception Ex)
            {
                return Result.Failure($"☠️ Error: {Ex.Message}");
            }
        }

        //Metodo para consultar
        public async Task<ResultList<ModeloDatos>> GetAll(string filtro = "")
        {
            try
            {
                var entities = await dbContext.Modelos
                    .Where(m => m.NombreM.ToLower().Contains(filtro.ToLower()))
                    .Select(m => m.ToDatos())
                    .ToListAsync();
                return ResultList<ModeloDatos>.Success(entities);
            }
            catch (Exception Ex)
            {
                return ResultList<ModeloDatos>.Failure($"☠️ Error: {Ex.Message}");
            }
        }
    }
}
