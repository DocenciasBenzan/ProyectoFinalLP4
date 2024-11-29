using APP2024P4.Data.Dtos;
using APP2024P4.Data.Entities;
using APP2024P4.Data;
using static APP2024P4.services.ICategoriaService;
using Microsoft.EntityFrameworkCore;

namespace APP2024P4.services;

public partial class CategoriaService : ICategoriaService
{
    private readonly IApplicationDbContext DbContext;
    public CategoriaService(ApplicationDbContext dbContext)
    {
        DbContext = dbContext;
    }

    //crud
    public async Task<Result> Create(CategoriaRequest ctg)
    {
        try
        {
            var entity = Categoria.Create(ctg.Nombre);
            DbContext.Categorias.Add(entity);
            await DbContext.SaveChangesAsync();
            return Result.Success("✅Producto registrado con exito!");
        }
        catch (Exception Ex)
        {
            return Result.Failure($"☠️ Error: {Ex.Message}");
        }
    }
    public async Task<Result> Update(CategoriaRequest ctg)
    {
        try
        {
            var entity = DbContext.Categorias.Where(c => c.Id == ctg.Id).FirstOrDefault();
            if (entity == null)
                return Result.Failure($"El ctg '{ctg.Id}' no existe!");
            if (entity.Update(ctg.Nombre))
            {
                await DbContext.SaveChangesAsync();
                return Result.Success("✅ctg modificado con exito!");
            }
            return Result.Success("🐫 No has realizado ningun cambio!");
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
            var entity = DbContext.Categorias.Where(p => p.Id == Id).FirstOrDefault();
            if (entity == null)
                return Result.Failure($"El producto '{Id}' no existe!");
            DbContext.Categorias.Remove(entity);
            await DbContext.SaveChangesAsync();
            return Result.Success("✅Producto eliminado con exito!");
        }
        catch (Exception Ex)
        {
            return Result.Failure($"☠️ Error: {Ex.Message}");
        }
    }
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