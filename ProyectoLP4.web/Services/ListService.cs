
using ProyectoLP4.web.Models;
using Microsoft.EntityFrameworkCore;
using ProyectoLP4.web.Data;


public class ListService : IListService
{
    //**SEGUNDA PRUEBA**
    public ListService(IApplicationDbContext context)
    {
        this.context = context;
    }
    private readonly List<UserList> _userLists = new();
    private readonly IApplicationDbContext context;

    public async Task<List<UserList>> GetListsAsync()
    {
        return await context.UserLists.Include(x => x.Movies).ToListAsync();
    }

    public Task CrearListaAsync(string nombre)
    {
        // agregar peliculas
        var list = new UserList { Name = nombre };
        context.UserLists.Add(list);
        return context.SaveChangesAsync();
    }

    public async Task AddMovieToListAsync(int listaId, Movie movie)
    {

        var lista = await context.UserLists.FirstOrDefaultAsync(l => l.Id == listaId);
        if (lista != null)
        {
            context.Movies.Add(movie);
            await context.SaveChangesAsync();
        }
    }

    public async Task<UserList?> GetListByIdAsync(int listaId)
    {
        var r = await context.UserLists.Include(x => x.Movies).FirstOrDefaultAsync(l => l.Id == listaId);
        return r;
    }

    //**PRIMERA PRUEBA**

    //private readonly ApplicationDbContext _context;

    //public ListService(ApplicationDbContext context)
    //{
    //_context = context;
    //}

    //Para obtener las listas
    //public async Task<List<UserList>> GetAllListsAsync()
    //{
    //return await _context.UserLists.Include(ul => ul.Movies).ToListAsync();
    //}

    //Para crear listas
    //public async Task CrearListaAsync (string listName)
    //{
    //if(string.IsNullOrEmpty(listName))
    //{
    //var newList = new UserList { Nombre = listName };
    //_context.UserLists.Add(newList);
    //await _context.SaveChangesAsync();
    //}
    //}

    //Función para agregar un título a una lista
    //public async Task AgregarTituloALista(int listID, Movie movie)
    //{
    //var lista = await _context.UserLists.FindAsync(listID);
    //if (lista != null)
    //{
    //lista.Movies.Add(movie);
    //await _context.SaveChangesAsync();
    //}
    //}
}
