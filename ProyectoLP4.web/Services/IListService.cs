using ProyectoLP4.web;
using ProyectoLP4.web.Models;

public interface IListService
{
    Task<Result> AddMovieToListAsync(int listaId, Movie movie);
    Task<Result> CrearListaAsync(string nombre);
    Task<Result<UserList>> GetListByIdAsync(int listaId);
    Task<ResultList<UserList>> GetListsAsync();

    Task<Result> UpdateListAsync(int listaId, string nuevoNombre);
    Task<Result> DeleteListAsync(int listaId);
    Task<Result> DeleteMovieFromListAsync(int movieId);
}