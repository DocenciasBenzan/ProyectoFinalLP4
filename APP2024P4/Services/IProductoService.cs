using APP2024P4.Data.Datos;
using APP2024P4.Data.Entities;

namespace APP2024P4.Services;

public interface IProductoService
{
    Task<Result> Create(ProductoRequest producto);
    Task<Result> Delete(int Id);
    Task<ResultList<ProductoDato>> Get(string filtro = "");
    Task<Result<ProductoDato>> GetById(int Id);
    Task<Result> Update(ProductoRequest producto);
}