using ProyectoLP4.web.Models;

public interface IListService
{
    Task AddMovieToListAsync(int listaId, Movie movie);
    Task CrearListaAsync(string nombre);
    Task<UserList?> GetListByIdAsync(int listaId);
    Task<List<UserList>> GetListsAsync();
}