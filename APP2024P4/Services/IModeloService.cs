using APP2024P4.Data.Datos;

namespace APP2024P4.Services
{
    //Interfaz de Modelo
    public interface IModeloService
    {
        Task<Result> Create(ModeloRequest modelo);
        Task<Result> Delete(int Id);
        Task<ResultList<ModeloDatos>> GetAll(string filtro = "");
        Task<Result> Update(ModeloRequest modelo);
    }
}