
using ProyectoLP4.web.Models;
using Microsoft.EntityFrameworkCore;
using ProyectoLP4.web.Data;
using ProyectoLP4.web;


public class ListService : IListService
{
    public ListService(IApplicationDbContext context)
    {
        this.context = context;
    }
    private readonly IApplicationDbContext context;

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
            return Result<UserList>.Failure($"Algo fallo al momento de obtenr la lista: {ex.Message}");
        }
    }

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
