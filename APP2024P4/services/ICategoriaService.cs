using APP2024P4.Data.Dtos;

namespace APP2024P4.services;

public interface ICategoriaService
{
   
        Task<Result> Create(CategoriaRequest categoria);
        Task<Result> Delete(int Id);
        Task<ResultList<CategoriaDto>> GetAll(string filtro = "");
        Task<Result> Update(CategoriaRequest categoria);
}
