using APP2024P4.Data.Datos;
using APP2024P4.Data.Entities;
using APP2024P4.Data;
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
        public async Task<Result> Create(CategoriaRequest categoria)
        {
            try
            {
                var entity = Categoria.Create(categoria.Nombre);
                dbContext.Categorias.Add(entity);
                await dbContext.SaveChangesAsync();
                return Result.Success("✅Categoria registrada con exito!");
            }
            catch (Exception Ex)
            {
                return Result.Failure($"☠️ Error: {Ex.Message}");
            }
        }
        public async Task<Result> Update(CategoriaRequest categoria)
        {
            try
            {
                var entity = dbContext.Categorias.Where(c => c.Id == categoria.Id).FirstOrDefault();
                if (entity == null)
                    return Result.Failure($"La Categoria'{categoria.Id}' no existe!");
                if (entity.Update(categoria.Nombre))
                {
                    await dbContext.SaveChangesAsync();
                    return Result.Success("✅Categoria modificada con exito!");
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
                var entity = dbContext.Categorias.Where(c => c.Id == Id).FirstOrDefault();
                if (entity == null)
                    return Result.Failure($"la Categoria '{Id}' no existe!");
                dbContext.Categorias.Remove(entity);
                await dbContext.SaveChangesAsync();
                return Result.Success("✅Categoria eliminada con exito!");
            }
            catch (Exception Ex)
            {
                return Result.Failure($"☠️ Error: {Ex.Message}");
            }
        }
        public async Task<ResultList<CategoriaDatos>> GetAll(string filtro = "")
        {
            try
            {
                var entities = await dbContext.Categorias
                    .Where(c => c.NombreC.ToLower().Contains(filtro.ToLower()))
                    .Select(c => new CategoriaDatos(c.Id, c.NombreC))
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
