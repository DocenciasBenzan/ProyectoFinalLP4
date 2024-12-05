using APP2024P4.Data.Dtos;
using APP2024P4.Data.Entities;
using APP2024P4.Data;
using static APP2024P4.services.ICategoriaService;
using Microsoft.EntityFrameworkCore;

namespace APP2024P4.services
{
    public partial class CategoriaService : ICategoriaService
    {
        private readonly IApplicationDbContext DbContext;

        public CategoriaService(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        /// <summary>
        /// Crea una nueva categoría.
        /// </summary>
        /// <param name="ctg">Detalles de la categoría a crear.</param>
        /// <returns>Resultado de la operación, indicando si fue exitosa o si ocurrió un error.</returns>
        public async Task<Result> Create(CategoriaRequest ctg)
        {
            try
            {
                var entity = Categoria.Create(ctg.Nombre);
                DbContext.Categorias.Add(entity);
                await DbContext.SaveChangesAsync();
                return Result.Success("✅Categoría registrada con éxito!");
            }
            catch (Exception Ex)
            {
                return Result.Failure($"☠️ Error: {Ex.Message}");
            }
        }

        /// <summary>
        /// Actualiza los detalles de una categoría existente.
        /// </summary>
        /// <param name="ctg">Detalles de la categoría a actualizar.</param>
        /// <returns>Resultado de la operación, indicando si fue exitosa o si no se realizaron cambios.</returns>
        public async Task<Result> Update(CategoriaRequest ctg)
        {
            try
            {
                var entity = DbContext.Categorias.Where(c => c.Id == ctg.Id).FirstOrDefault();
                if (entity == null)
                    return Result.Failure($"La categoría '{ctg.Id}' no existe!");
                if (entity.Update(ctg.Nombre))
                {
                    await DbContext.SaveChangesAsync();
                    return Result.Success("✅Categoría modificada con éxito!");
                }
                return Result.Success("🐫 No has realizado ningún cambio!");
            }
            catch (Exception Ex)
            {
                return Result.Failure($"☠️ Error: {Ex.Message}");
            }
        }

        /// <summary>
        /// Elimina una categoría por su ID.
        /// </summary>
        /// <param name="Id">ID de la categoría a eliminar.</param>
        /// <returns>Resultado de la operación, indicando si fue exitosa o si la categoría no existe.</returns>
        public async Task<Result> Delete(int Id)
        {
            try
            {
                var entity = DbContext.Categorias.Where(p => p.Id == Id).FirstOrDefault();
                if (entity == null)
                    return Result.Failure($"La categoría '{Id}' no existe!");
                DbContext.Categorias.Remove(entity);
                await DbContext.SaveChangesAsync();
                return Result.Success("✅Categoría eliminada con éxito!");
            }
            catch (Exception Ex)
            {
                return Result.Failure($"☠️ Error: {Ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene una lista de categorías filtradas por nombre.
        /// </summary>
        /// <param name="filtro">Texto para filtrar categorías por nombre (opcional).</param>
        /// <returns>Resultado con la lista de categorías encontradas.</returns>
        public async Task<ResultList<CategoriaDto>> GetAll(string filtro = "")
        {
            try
            {
                var entities = await DbContext.Categorias
                    .Where(p => p.Nombre.ToLower().Contains(filtro.ToLower()))
                    .Select(p => new CategoriaDto(p.Id, p.Nombre))
                    .ToListAsync();
                return ResultList<CategoriaDto>.Success(entities);
            }
            catch (Exception Ex)
            {
                return ResultList<CategoriaDto>.Failure($"☠️ Error: {Ex.Message}");
            }
        }
    }
}
