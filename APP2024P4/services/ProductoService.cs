namespace APP2024P4.Services;

using APP2024P4.Data.Dtos;
using APP2024P4.Data.Entities;
using APP2024P4.Data;
using APP2024P4.services;
using Microsoft.EntityFrameworkCore;
using APP2024P4;
using APP2024P4.Data.dbcontext;

public partial class ProductoService : IProductoService
{
    private readonly IDatabaseApp DbContext;

    public ProductoService(IDatabaseApp dbContext)
    {
        DbContext = dbContext;
    }
    //CRUD
    public async Task<Result> Create(ProductoRequest producto)
    {
        try
        {
            var entity = Producto.Create(producto.Nombre, producto.Img, producto.CategoriaId, producto.Precio);
            DbContext.Productos.Add(entity);
            await DbContext.SaveChangesAsync();
            return Result.Success("✅Producto registrado con exito!");
        }
        catch (Exception Ex)
        {
            return Result.Failure($"☠️ Error: {Ex.Message}");
        }
    }
    public async Task<Result> Update(ProductoRequest producto)
    {
        try
        {
            var entity = DbContext.Productos.Where(p => p.Id == producto.Id).FirstOrDefault();
            if (entity == null)
                return Result.Failure($"El producto '{producto.Id}' no existe!");
            if (entity.Update(producto.Nombre, producto.Img!, producto.CategoriaId, producto.Precio))
            {
                await DbContext.SaveChangesAsync();
                return Result.Success("✅Producto modificado con exito!");
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
            var entity = DbContext.Productos.Where(p => p.Id == Id).FirstOrDefault();
            if (entity == null)
                return Result.Failure($"El producto '{Id}' no existe!");
            DbContext.Productos.Remove(entity);
            await DbContext.SaveChangesAsync();
            return Result.Success("✅Producto eliminado con exito!");
        }
        catch (Exception Ex)
        {
            return Result.Failure($"☠️ Error: {Ex.Message}");
        }
    }
    public async Task<Result<ProductoDto>> GetById(int Id)
    {
        try
        {   
            var entity = await DbContext.Productos.Where(p => p.Id == Id)
                .Select(p => new ProductoDto(p.Id, p.Nombre, p.Img, p.CategoriaId, p.Categoria!.Nombre ?? "No definida", p.Precio))
                .FirstOrDefaultAsync();
            if (entity == null)
                return Result<ProductoDto>.Failure($"El producto '{Id}' no existe!");

            return Result<ProductoDto>.Success(entity);
        }
        catch (Exception Ex)
        {
            return Result<ProductoDto>.Failure($"☠️ Error: {Ex.Message}");
        }
    }
    public async Task<ResultList<ProductoDto>> Get(string filtro = "")
    {
        try
        {
            var entities = await DbContext.Productos
                .Where(p => p.Nombre.ToLower().Contains(filtro.ToLower()))
                .Select(p => new ProductoDto(p.Id, p.Nombre, p.Img, p.CategoriaId, p.Categoria!.Nombre ?? "No definida", p.Precio))
                .ToListAsync();
            return ResultList<ProductoDto>.Success(entities);
        }
        catch (Exception Ex)
        {
            return ResultList<ProductoDto>.Failure($"☠️ Error: {Ex.Message}");
        }
    }
}