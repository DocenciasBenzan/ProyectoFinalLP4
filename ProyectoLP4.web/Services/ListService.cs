
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


    // public async Task<List<UserList>> GetListsAsync()
    // {

    //     // Esto tendria que tener validacion :/
    //     var r =  context.UserLists.ToList();

    //     if (r.Count == 0) {
    //         return new List<UserList>();
    //     }

    //     return await context.UserLists.Include(x => x.Movies).ToListAsync();
    // }

    // public Task CrearListaAsync(string nombre)
    // {
    //     // agregar peliculas
    //     var list = new UserList { Name = nombre };
    //     context.UserLists.Add(list);
    //     return context.SaveChangesAsync();
    // }

    // public async Task AddMovieToListAsync(int listaId, Movie movie)
    // {

    //     var lista = await context.UserLists.FirstOrDefaultAsync(l => l.Id == listaId);
    //     if (lista != null)
    //     {
    //movie.UserListId = listaId;
    //movie.UserList = lista;
    //context.Movies.Add(movie);
    //         await context.SaveChangesAsync();
    //     }
    // }

    // public async Task<UserList?> GetListByIdAsync(int listaId)
    // {
    //     var r = await context.UserLists.Include(x => x.Movies).FirstOrDefaultAsync(l => l.Id == listaId);
    //     return r;
    // }



}
