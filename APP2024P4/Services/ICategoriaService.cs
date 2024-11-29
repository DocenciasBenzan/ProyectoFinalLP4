using APP2024P4.Data.Datos;

namespace APP2024P4.Services
{
    public interface ICategoriaService
    {
        Task<Result> Create(CategoriaRequest categoria);
        Task<Result> Delete(int Id);
        Task<ResultList<CategoriaDatos>> GetAll(string filtro = "");
        Task<Result> Update(CategoriaRequest categoria);
    }
}