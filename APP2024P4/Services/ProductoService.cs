namespace APP2024P4.Services;

using Microsoft.EntityFrameworkCore;
using APP2024P4;
using APP2024P4.Data;
using APP2024P4.Data.Datos;
using APP2024P4.Data.Entities;


public partial class ProductoService : IProductoService
{
    private readonly IApplicationDbContext dbContext;

    public ProductoService(IApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    //CRUD

    public async Task<Result> Create(ProductoRequest producto)
    {
        try
        {
            var entity = Producto.Create(producto.Nombre, producto.CategoriaId, producto.FechaL, producto.Color, producto.Cantidad, producto.Descripcion, producto.ModeloId, producto.Precio, producto.Imagen);
            dbContext.Productos.Add(entity);
            await dbContext.SaveChangesAsync();
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
            var entity = dbContext.Productos.Where(p => p.Id == producto.Id).FirstOrDefault();
            if (entity == null)
                return Result.Failure($"El producto '{producto.Id}' no existe!");
            if (entity.Update(producto.Nombre, producto.CategoriaId, producto.FechaL, producto.Color, producto.Cantidad, producto.Descripcion, producto.ModeloId, producto.Precio, producto.Imagen))
            {
                await dbContext.SaveChangesAsync();
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
            var entity = dbContext.Productos.Where(p => p.Id == Id).FirstOrDefault();
            if (entity == null)
                return Result.Failure($"El producto '{Id}' no existe!");
            dbContext.Productos.Remove(entity);
            await dbContext.SaveChangesAsync();
            return Result.Success("✅Producto eliminado con exito!");
        }
        catch (Exception Ex)
        {
            return Result.Failure($"☠️ Error: {Ex.Message}");
        }
    }
    public async Task<Result<ProductoDato>> GetById(int Id)
    {
        try
        {
            var entity = await dbContext.Productos.Where(p => p.Id == Id)
                .Select(p => new ProductoDato(p.Id, p.Nombre, p.CategoriaId, p.Categoria!.NombreC ?? "No definida", p.FechaL, p.Color, p.Cantidad , p.ModeloId , p.Modelo!.NombreM ?? "No definida", p.Precio, p.Descripcion, p.Imagen))
                .FirstOrDefaultAsync();
            if (entity == null)
                return Result<ProductoDato>.Failure($"El producto '{Id}' no existe!");

            return Result<ProductoDato>.Success(entity);
        }
        catch (Exception Ex)
        {
            return Result<ProductoDato>.Failure($"☠️ Error: {Ex.Message}");
        }
    }
    public async Task<ResultList<ProductoDato>> Get(string filtro = "")
    {
        try
        {
            var entities = await dbContext.Productos
                .Where(p => p.Nombre.ToLower().Contains(filtro.ToLower()))
                .Select(p => new ProductoDato(p.Id, p.Nombre, p.CategoriaId, p.Categoria!.NombreC ?? "No definida", p.FechaL, p.Color, p.Cantidad, p.ModeloId, p.Modelo!.NombreM ?? "No definida", p.Precio, p.Descripcion, p.Imagen))
                .ToListAsync();
            return ResultList<ProductoDato>.Success(entities);
        }
        catch (Exception Ex)
        {
            return ResultList<ProductoDato>.Failure($"☠️ Error: {Ex.Message}");
        }
    }
}
