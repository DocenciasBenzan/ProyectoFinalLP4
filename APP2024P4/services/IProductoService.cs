using APP2024P4.Data.Dtos;

namespace APP2024P4.services
{
    public interface IProductoService
    {
        /// <summary>
        /// Crea un nuevo producto.
        /// </summary>
        Task<Result> Create(ProductoRequest producto);

        /// <summary>
        /// Elimina un producto por su ID.
        /// </summary>
        Task<Result> Delete(int Id);

        /// <summary>
        /// Obtiene una lista de productos con opción de filtro.
        /// </summary>
        Task<ResultList<ProductoDto>> Get(string filtro = "");

        /// <summary>
        /// Obtiene un producto por su ID.
        /// </summary>
        Task<Result<ProductoDto>> GetById(int Id);

        /// <summary>
        /// Actualiza los detalles de un producto existente.
        /// </summary>
        Task<Result> Update(ProductoRequest producto);
    }
}
