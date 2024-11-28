using APP2024P4.Data.Dtos;


namespace APP2024P4.services;

public interface IProductoService
{
    Task<Result> Create(ProductoRequest producto);
    Task<Result> Delete(int Id);
    Task<ResultList<ProductoDto>> Get(string filtro = "");
    Task<Result<ProductoDto>> GetById(int Id);
    Task<Result> Update(ProductoRequest producto);
}