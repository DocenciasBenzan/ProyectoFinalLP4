namespace APP2024P4.Services
{
    using Microsoft.EntityFrameworkCore;
    using APP2024P4.Data;
    using APP2024P4.Data.Dtos;
    using APP2024P4.Data.Entities;
    using APP2024P4;

    public interface IProductoService
    {
        Task<Result> Create(ProductoRequest producto);
        Task<Result> Delete(int Id);
        Task<Result> Get(string filtro = "");
        Task<Result> GetById(int Id);
        Task<Result> Update(ProductoRequest producto);
    }
    public partial class ProductoService : IProductoService
    {
        private readonly IApplicationDbContext dbContext;
        public ProductoService(IApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // CRUD
        public async Task<Result> Create(ProductoRequest producto)
        {
            try
            {
                var entity = Producto.Create(producto.Nombre, producto.Descripcion,
                    producto.CategoriaId, producto.Precio);
                dbContext.Productos.Add(entity);
                await dbContext.SaveChangesAsync();
                return Result.Success("Ah sido registrado exitosamente.");
            }
            catch (Exception VariableParaCapturarError)
            {
                return Result.Failure($"Error: {VariableParaCapturarError.Message}");
            }
        }
        public async Task<Result> Update(ProductoRequest producto)
        {
            try
            {
                var entity = dbContext.Productos.Where(p => p.Id == producto.Id).FirstOrDefault();
                if (entity == null)
                {
                    return Result.Failure($"El producto '{producto.Id}' no existe.");
                }


                if (entity.Update(producto.Nombre, producto.Descripcion,
                    producto.CategoriaId, producto.Precio))
                {
                    await dbContext.SaveChangesAsync();
                    return Result.Success("Producto modificado con exito.");
                }
                return Result.Success("No has realizado ningun cambio");
            }
            catch (Exception VariableParaCapturarError)
            {
                return Result.Failure($"Error: {VariableParaCapturarError.Message}");
            }
        }
        public async Task<Result> Delete(int Id)
        {
            try
            {
                var entity = dbContext.Productos.Where(p => p.Id == Id).FirstOrDefault();
                if (entity == null)
                {
                    return Result.Failure($"El producto '{Id}' no existe.");
                }
                dbContext.Productos.Remove(entity);
                await dbContext.SaveChangesAsync();
                return Result.Success("Producto eliminado con exito.");

            }
            catch (Exception VariableParaCapturarError)
            {
                return Result.Failure($"Error: {VariableParaCapturarError.Message}");
            }
        }
        public async Task<Result<ProductoDto>> GetById(int Id)
        {
            try
            {
                var entity = await dbContext.Productos.Where(p => p.Id == Id)
                    .Select(p => new ProductoDto(p.Id, p.Nombre, p.Descripcion,
                    p.CategoriaId, p.Categoria!.Nombre?? "No definida", p.Precio))
                    .FirstOrDefaultAsync();
                if(entity == null)
                {
                    return Result<ProductoDto>.Failure($"El producto '{Id}' no existe");
                }
                return Result<ProductoDto>.Success(entity);
            }
            catch (Exception VariableParaCapturarError)
            {
                return Result<ProductoDto>.Failure($"Error: {VariableParaCapturarError.Message}");
            }
        }
        public async Task<ResultList<ProductoDto>> Get(string filtro = "")
        {
            try
            {
                var entities = await dbContext.Productos
                    .Where(p => p.Nombre.ToLower().Contains(filtro.ToLower()))
                    .Select(p => new ProductoDto(p.Id, p.Nombre, p.Descripcion, p.CategoriaId,
                    p.Categoria!.Nombre ?? "No definida", p.Precio))
                    .ToListAsync();
                return ResultList<ProductoDto>.Success(entities);
            }
            catch (Exception VariableParaCapturarError)
            {
                return ResultList<ProductoDto>.Failure($"Error: {VariableParaCapturarError.Message}");
            }
        }
    }
}
