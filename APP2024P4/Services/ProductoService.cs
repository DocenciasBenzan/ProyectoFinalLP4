using Microsoft.EntityFrameworkCore;
using APP2024P4.Data;
using APP2024P4.Data.Dtos;
using APP2024P4.Data.Entities;

namespace APP2024P4.Services
{
    
    public partial class ProductoService : IProductoService
    {
        private readonly IApplicacionDbContext dbContext;
        public ProductoService(IApplicacionDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /* CRUD */
        public async Task<Resultado> Create(string nombre, string? descripcion)
        {
            try
            {
                var entity = Producto.Create(nombre, descripcion);
                dbContext.Productos.Add(entity);
                await dbContext.SaveChangesAsync();
                return Resultado.Success("Ah sido registrado exitosamente.");
            }
            catch (Exception)
            {
                return Resultado.Failure($"Error: {Ex.Message}");
            }
        }
        public async Task<Resultado> Update(int Id, string nombre, string? descripcion)
        {
            try
            {
                var entity = dbContext.Productos.Where(p => p.Id == Id).FirstOrDefault();
                if (entity == null)
                {
                    return Resultado.Failure($"El producto '{Id}' no existe.");
                }
                if (entity.Update(nombre, descripcion))
                {
                    await dbContext.SaveChangesAsync();
                    return Resultado.Success("Producto modificado con exito.");
                }
                return Resultado.Success("No has realizado ningun cambio");
            }
            catch (Exception Ex)
            {
                return Resultado.Failure($"Error: {Ex.Message}");
            }
        }
        public async Task<Resultado> Delete(int Id)
        {
            try
            {
                var entity = dbContext.Productos.Where(p => p.Id == Id).FirstOrDefault();
                if (entity == null)
                {
                    return Resultado.Failure($"El producto '{Id}' no existe.");
                    dbContext.Productos.Remove(entity);
                    await dbContext.SaveChangesAsync();
                    return Resultado.Success("Producto eliminado con exito.");
                }
                    
            }
            catch (Exception Ex)
            {
                return Resultado.Failure($"Error: {Ex.Message}");
            }
        }
        public async Task<Resultado<ProductoDto>> GetById(int Id)
        {
            try
            {
                var entity = await dbContext.Productos.Where(p => p.Id == Id)
                    .Select(p => new ProductoDto(p.Id, p.Nombre, p.Descripcion))
                    .FirstOrDefaultAsync();
                if(entity == null)
                {
                    return Resultado<ProductoDto>.Failure($"El producto '{Id}' no existe");
                }
                return Resultado<ProductoDto>.Success(entity);
            }
            catch(Exception Ex)
            {
                public async Task<ResultadoList<ProductoDto>> Get(string filtro = "")
                {
                    try
                    {
                        var entities = await dbContext.Productos.Where(p => p.Nombre.ToLower().Contains(filtro.ToLower()))
                            .Select(p => new ProductoDto(p.Id, p.Nombre, p.Descripcion))
                            .ToListAsync();
                        return ResultadoList<ProductoDto>.Success(entities);
                    }
                    catch (Exception Ex)
                    {
                        return ResultadoList<ProductoDto>.Failure($"Error: {Ex.Message}");
                    }
                }
            }
        }
    }
}
