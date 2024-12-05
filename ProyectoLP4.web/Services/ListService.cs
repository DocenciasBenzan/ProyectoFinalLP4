
using ProyectoLP4.web.Models;
using Microsoft.EntityFrameworkCore;
using ProyectoLP4.web.Data;
using ProyectoLP4.web;

/// <summary>
/// Servicio para el gestionamiento de las listas
/// </summary>
public class ListService : IListService
{

    /// <summary>
    /// Metodo para poder tener conexion con el AppDbContext.
    /// </summary>
    /// <param name="context"></param>
    /// Variable usada para establecer dicha conexión.
    public ListService(IApplicationDbContext context)
    {
        this.context = context;
    }
    private readonly IApplicationDbContext context;

    /// <summary>
    ///     Función para obtener todas las listas existentes en la base de datos.
    /// </summary>
    /// <returns>Todas las listas ya creadas</returns>
    public async Task<ResultList<UserList>> GetListsAsync()
    {
        try
        {
            var listas = context.UserLists.AsNoTracking().Include(x=>x.Movies).ToList();
            if (listas.Count == 0)
            {
                return ResultList<UserList>.Failure("Sin listas encontradas");
            }
           
            return ResultList<UserList>.Success(listas);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return ResultList<UserList>.Failure($"Ocurrio un error al obtener las listas ::: {ex.Message}");
        }
    }
    
    /// <summary>
    /// Función para crear listas nuevas
    /// </summary>
    /// <param name="nombre"></param>
    /// <returns>Nombre de la lista</returns>
    public async Task<Result> CrearListaAsync(string nombre)
    {
        try
        {
            var list = new UserList { Name = nombre };
            context.UserLists.Add(list);
            await context.SaveChangesAsync();
            return Result.Success("Lista agregada");
        }
        catch (Exception ex)
        {
            return Result.Failure($"Error al crear la lista : {ex.Message}");
        }
    }

    /// <summary>
    /// Función para agregar titulos a una lista (ambas: peliculas y series).
    /// </summary>
    /// <param name="listaId"></param>
    /// <param name="movie"></param>
    /// <returns>Parametros que contienen el ID de la lista seleccionada y la información de la pelicula a guardar.</returns>
    public async Task<Result> AddMovieToListAsync(int listaId, Movie movie)
    {
        try
        {
            var lista = await context.UserLists.FirstOrDefaultAsync(l => l.Id == listaId);
            if (lista != null)
            {
                movie.UserListId = listaId;
                movie.UserList = lista;
                context.Movies.Add(movie);
                await context.SaveChangesAsync();
                return Result.Success("Pelicula agregada");
            }
            return Result.Failure(" Lista no encontrada");
        }
        catch (Exception ex)
        {
            return Result.Failure($"ALGO FALLO AL AGREGAR LA PELICULA: {ex.Message}");
        }
    }

    /// <summary>
    /// Para obtener las listas por su ID.
    /// </summary>
    /// <param name="listaId"></param>
    /// <returns>El ID único de la lista.</returns>
    public async Task<Result<UserList>> GetListByIdAsync(int listaId)
    {
        try
        {
            var r = await context.UserLists.AsNoTracking().Include(x => x.Movies).FirstOrDefaultAsync(l => l.Id == listaId);
           if(r == null)
            {
                return Result<UserList>.Failure("Lista no encontrada :/");
            }
           return Result<UserList>.Success(r);

        }
        catch (Exception ex)
        {
            return Result<UserList>.Failure($"Algo fallo al momento de obtener la lista: {ex.Message}");
        }
    }


    /// <summary>
    /// Para actualizar listas (Editar nombre).
    /// </summary>
    /// <param name="listaId"></param>
    /// <param name="nuevoNombre"></param>
    /// <returns>Representan el ID y el nuevo nombre de la lista.</returns>
	public async Task<Result> UpdateListAsync(int listaId, string nuevoNombre)
	{
		try
		{
			var lista = await context.UserLists.FirstOrDefaultAsync(l => l.Id == listaId);
			if (lista == null)
			{
				return Result.Failure("List not found.");
			}

			lista.Name = nuevoNombre;
			await context.SaveChangesAsync();

			return Result.Success("List renamed successfully.");
		}
		catch (Exception ex)
		{
			return Result.Failure($"Error trying to rename the list: {ex.Message}");
		}
	}

    /// <summary>
    /// Para eliminar una lista.
    /// </summary>
    /// <param name="listaId"></param>
    /// <returns>ID de la lista.</returns>
	public async Task<Result> DeleteListAsync(int listaId)
	{
		try
		{
			var lista = await context.UserLists.Include(l => l.Movies).FirstOrDefaultAsync(l => l.Id == listaId);
			if (lista == null)
			{
				return Result.Failure("List not found or already deleted");
			}

			context.Movies.RemoveRange(lista.Movies);
			context.UserLists.Remove(lista);
			await context.SaveChangesAsync();

			return Result.Success("List deleted successfully.");
		}
		catch (Exception ex)
		{
			return Result.Failure($"Error trying to delete list: {ex.Message}");
		}
	}
    
    /// <summary>
    /// Elimina una pelicula de una lista.
    /// </summary>
    /// <param name="movieId"></param>
    /// <returns>ID del titulo a eliminar.</returns>
	public async Task<Result> DeleteMovieFromListAsync(int movieId)
	{
		try
		{
			var movie = await context.Movies.FirstOrDefaultAsync(m => m.Id == movieId);
			if (movie == null)
			{
				return Result.Failure("Movie or tv show not found.");
			}

			context.Movies.Remove(movie);
			await context.SaveChangesAsync();

			return Result.Success("Movie or tv show removed succesfully.");
		}
		catch (Exception ex)
		{
			return Result.Failure($"Error trying to remove the movie or tv show: {ex.Message}");
		}
	}
}
