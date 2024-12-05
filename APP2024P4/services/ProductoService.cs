namespace APP2024P4.Services;

using APP2024P4.Data.Dtos;
using APP2024P4.Data.Entities;
using APP2024P4.Data;
using APP2024P4.services;
using Microsoft.EntityFrameworkCore;
using APP2024P4;

public partial class ProductoService : IProductoService
{
    private readonly IApplicationDbContext DbContext;

    public ProductoService(IApplicationDbContext dbContext)
    {
        DbContext = dbContext;
    }

    /// <summary>
    /// Crea un nuevo producto en la base de datos.
    /// </summary>
    /// <param name="producto">Detalles del producto a crear.</param>
    /// <returns>Resultado de la operación, indicando si fue exitosa o si ocurrió un error.</returns>
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

    /// <summary>
    /// Actualiza los detalles de un producto existente en la base de datos.
    /// </summary>
    /// <param name="producto">Detalles del producto a actualizar.</param>
    /// <returns>Resultado de la operación, indicando si fue exitosa o si no se hicieron cambios.</returns>
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

    /// <summary>
    /// Elimina un producto de la base de datos por su ID.
    /// </summary>
    /// <param name="Id">ID del producto a eliminar.</param>
    /// <returns>Resultado de la operación, indicando si fue exitosa o si el producto no existe.</returns>
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

    /// <summary>
    /// Obtiene los detalles de un producto por su ID.
    /// </summary>
    /// <param name="Id">ID del producto a obtener.</param>
    /// <returns>Resultado con los detalles del producto o mensaje de error si no se encuentra.</returns>
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

    /// <summary>
    /// Obtiene una lista de productos con un filtro opcional por nombre.
    /// </summary>
    /// <param name="filtro">Texto para filtrar productos por nombre (opcional).</param>
    /// <returns>Resultado con una lista de productos filtrados.</returns>
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
