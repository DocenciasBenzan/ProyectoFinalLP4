using ProyectoLP4.web;
using ProyectoLP4.web.Models;

public interface IListService
{
    Task<Result> AddMovieToListAsync(int listaId, Movie movie);
    Task<Result> CrearListaAsync(string nombre);
    Task<Result<UserList>> GetListByIdAsync(int listaId);
    Task<ResultList<UserList>> GetListsAsync();
}