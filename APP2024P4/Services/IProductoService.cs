using APP2024P4.Data.Datos;
using APP2024P4.Data.Entities;

namespace APP2024P4.Services;

//Interfaz de Porducto
public interface IProductoService
{
    Task<Result> Create(ProductoRequest producto);
    Task<Result> Delete(int Id);
    Task<ResultList<ProductoDatos>> Get(string filtro = "");
    Task<Result<ProductoDatos>> GetById(int Id);
    Task<Result> Update(ProductoRequest producto);
}