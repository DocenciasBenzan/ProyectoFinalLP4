using APP2024P4.Data;
using APP2024P4.Data.Dtos;

namespace APP2024P4.Services
{
    public interface IProductoService
    {
        Task<Resultado> Create(string nombre, string? descripcion);
        Task<Resultado> Delete(int Id);
        Task<ResultadoList<ProductoDto>> Get(string filtro = "");
        Task<Resultado<ProductoDto>> GetById(int Id);
        Task<Resultado> Update(int Id, string nombre, string? descripcion);
    }
}
