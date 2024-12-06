using APP2024P4.Data.Datos;
using APP2024P4.Data.Entities;
using APP2024P4.Data;
using APP2024P4;
using Microsoft.EntityFrameworkCore;

namespace APP2024P4.Services
{
    public partial class CategoriaService : ICategoriaService
    {
        private readonly IApplicationDbContext dbContext;

        public CategoriaService(IApplicationDbContext context)
        {
            this.dbContext = context;
        }

        //CRUD

        //Metodo para registrar
        public async Task<Result> Create(CategoriaRequest request)
        {
            try
            {
                var entity = Categoria.Create(request.Nombre);
                dbContext.Categorias.Add(entity);
                await dbContext.SaveChangesAsync();
                return Result.Success("✅Categoría registrada con exito!");
            }
            catch (Exception Ex)
            {
                return Result.Failure($"☠️ Error: {Ex.Message}");
            }
        }

        //Metodo para Editar
        public async Task<Result> Update(CategoriaRequest request)
        {
            try
            {
                var entity = dbContext.Categorias.Where(c => c.Id == request.Id).FirstOrDefault();
                if (entity == null)
                    return Result.Failure($"La categoría '{request.Id}' no existe!");
                if (entity.Update(request.Nombre))
                {
                    await dbContext.SaveChangesAsync();
                    return Result.Success("✅Categoría modificada con exito!");
                }
                return Result.Success("🐫 No has realizado ningun cambio!");
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
                var entity = dbContext.Categorias.Where(c => c.Id == Id).FirstOrDefault();
                if (entity == null)
                    return Result.Failure($"La categoría '{Id}' no existe!");
                dbContext.Categorias.Remove(entity);
                await dbContext.SaveChangesAsync();
                return Result.Success("✅Categoría eliminada con exito!");
            }
            catch (Exception Ex)
            {
                return Result.Failure($"☠️ Error: {Ex.Message}");
            }
        }

        //Metodo para consultar
        public async Task<ResultList<CategoriaDatos>> GetAll(string filtro = "")
        {
            try
            {
                var entities = await dbContext.Categorias
                    .Where(c => c.NombreC.ToLower().Contains(filtro.ToLower()))
                    .Select(c => c.ToDatos())
                    .ToListAsync();
                return ResultList<CategoriaDatos>.Success(entities);
            }
            catch (Exception Ex)
            {
                return ResultList<CategoriaDatos>.Failure($"☠️ Error: {Ex.Message}");
            }
        }
    }
}
