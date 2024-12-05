using APP2024P4.Data.Dtos;

namespace APP2024P4.services
{
    public interface ICategoriaService
    {
        /// <summary>
        /// Crea una nueva categoría.
        /// </summary>
        Task<Result> Create(CategoriaRequest categoria);

        /// <summary>
        /// Elimina una categoría por su ID.
        /// </summary>
        Task<Result> Delete(int Id);

        /// <summary>
        /// Obtiene todas las categorías, con opción de filtro.
        /// </summary>
        Task<ResultList<CategoriaDto>> GetAll(string filtro = "");

        /// <summary>
        /// Actualiza una categoría existente.
        /// </summary>
        Task<Result> Update(CategoriaRequest categoria);
    }
}
