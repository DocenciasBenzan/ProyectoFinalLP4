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

    //Metodo para registrar
    public async Task<Result> Create(ProductoRequest producto)
    {
        try
        {
            var entity = Producto.Create(producto);
            dbContext.Productos.Add(entity);
            await dbContext.SaveChangesAsync();
            return Result.Success("✅Producto registrado con exito!");
        }
        catch (Exception Ex)
        {
            return Result.Failure($"☠️ Error: {Ex.Message}");
        }
    }

    //Metodo para Editar
    public async Task<Result> Update(ProductoRequest producto)
    {
        try
        {
            var entity = dbContext.Productos.Where(p => p.Id == producto.Id).FirstOrDefault();
            if (entity == null)
                return Result.Failure($"El producto '{producto.Id}' no existe!");
            if (entity.Update(producto))
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

    //Metodo para eliminar
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

    //Metodo para obtener ID
    public async Task<Result<ProductoDatos>> GetById(int Id)
    {
        try
        {
            var entity = await dbContext.Productos.Where(p => p.Id == Id)
                .Select(p => p.ToDatos())
                .FirstOrDefaultAsync();
            if (entity == null)
                return Result<ProductoDatos>.Failure($"El producto '{Id}' no existe!");

            return Result<ProductoDatos>.Success(entity);
        }
        catch (Exception Ex)
        {
            return Result<ProductoDatos>.Failure($"☠️ Error: {Ex.Message}");
        }
    }

    //Metodo para consultar
    public async Task<ResultList<ProductoDatos>> Get(string filtro = "")
    {
        try
        {
            var entities = await dbContext.Productos
                .Where(p => p.Nombre.ToLower().Contains(filtro.ToLower()))
                .Select(p => p.ToDatos())
                .ToListAsync();
            return ResultList<ProductoDatos>.Success(entities);
        }
        catch (Exception Ex)
        {
            return ResultList<ProductoDatos>.Failure($"☠️ Error: {Ex.Message}");
        }
    }
}
