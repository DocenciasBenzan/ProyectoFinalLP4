using APP2024P4.Data.Datos;

namespace APP2024P4.Services
{
    public interface IMarcaService
    {
        Task<Result> Create(MarcaRequest marca);
        Task<Result> Delete(int Id);
        Task<ResultList<MarcaDatos>> GetAll(string filtro = "");
        Task<Result> Update(MarcaRequest marca);
    }
}