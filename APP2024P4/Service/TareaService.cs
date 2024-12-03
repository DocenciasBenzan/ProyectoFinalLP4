using APP2024P4.Data.Dtos;
using APP2024P4.Data.Entidades;
using APP2024P4.Data;
using Microsoft.AspNetCore.Identity;
using TaskMaster;
using Microsoft.EntityFrameworkCore;


namespace APP2024P4.Service;

public interface ITareaSevice
{
    Task<Result> Create(TareaRequest tarea, string UserId);
    Task<Result> Delete(int Id);
    Task<ResultList<TareaDto>> Get(string filtro = "");
    Task<ResultList<TareaDto>> GetById(string UserId);
    Task<Result> Update(TareaRequest tarea);
    Task<ResultList<TareaDto>> ObtenerTareasPorColaborador(string email);


}
public partial class TareaService : ITareaSevice
{
    private readonly IApplicationDbContext dbContext;
    private readonly UserManager<ApplicationUser> _userManager;

    public TareaService(IApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
    {
        this.dbContext = dbContext;
        _userManager = userManager;
    }
    public async Task<List<Tarea>> GetTasksForUserAsync(string userId)
    {
        return await dbContext.Tareas
            .Where(t => t.UserId == userId)
            .ToListAsync();
    }
    public async Task<Result> Create(TareaRequest tarea, string userId)
    {
        try
        {
            var entity = Tarea.Create(
                tarea.Titulo,
                tarea.FechaCreacion,
                tarea.FechaLimite,
                tarea.IsCompleted,
                tarea.Estado,
                tarea.Prioridad,
                tarea.Descripcion
                );
            entity.UserId = userId;
            dbContext.Tareas.Add(entity);
            await dbContext.SaveChangesAsync();
            return Result.Success("Tarea registrada con exito!");
        }
        catch (Exception Ex)
        {
            return Result.Failure($"Error: {Ex.Message}");
        }
    }
    public async Task<Result> Update(TareaRequest tarea)
    {
        try
        {
            var entity = dbContext.Tareas.Where(p => p.Id == tarea.Id).FirstOrDefault();
            if (entity == null)
                return Result.Failure($"La Tarea '{tarea.Id}' no existe!");
            if (entity.Update(tarea.Titulo,
                tarea.FechaCreacion,
                tarea.FechaLimite,
                tarea.IsCompleted,
                tarea.Estado,
                tarea.Prioridad,
                tarea.Descripcion
                ))
            {
                await dbContext.SaveChangesAsync();
                return Result.Success("La tarea actualizada con exito!");
            }
            return Result.Success("No has realizado ningun cambio!");
        }
        catch (Exception Ex)
        {
            return Result.Failure($"Error: {Ex.Message}");
        }
    }
    public async Task<Result> Delete(int Id)
    {
        try
        {
            var entity = dbContext.Tareas.Where(p => p.Id == Id).FirstOrDefault();
            if (entity == null)
                return Result.Failure($"La tarea '{Id}' no existe!");
            dbContext.Tareas.Remove(entity);
            await dbContext.SaveChangesAsync();
            return Result.Success("Tarea eliminada con exito!");
        }
        catch (Exception Ex)
        {
            return Result.Failure($"Error: {Ex.Message}");
        }
    }
    public async Task<ResultList<TareaDto>> GetById(string UserId)
    {
        try
        {
            var entity = await dbContext.Tareas.Where(p => p.UserId == UserId)
                .Select(t => new TareaDto(
                    t.Id,
                    t.UserId,
                    t.Titulo,
                    t.Descripcion!,
                    t.Estado,
                    t.Prioridad,
                    t.FechaCreacion,
                    t.FechaLimite,
                    t.IsCompleted,
                    string.Empty, 
                    new List<string>()
                    ))
                .ToListAsync();
            if (entity == null)
                return ResultList<TareaDto>.Failure($"El producto no existe!");

            return ResultList<TareaDto>.Success(entity);
        }
        catch (Exception Ex)
        {
            return ResultList<TareaDto>.Failure($"☠️ Error: {Ex.Message}");
        }
    }
    public async Task<ResultList<TareaDto>> Get(string filtro = "")
    {
        try
        {
            var entities = await dbContext.Tareas
                .Where(p => p.Titulo.ToLower().Contains(filtro.ToLower()))
                .Select(t => new TareaDto(
                    t.Id,
                    t.UserId,
                    t.Titulo,
                    t.Descripcion!,
                    t.Estado,
                    t.Prioridad,
                    t.FechaCreacion,
                    t.FechaLimite,
                    t.IsCompleted,
                    string.Empty,
                    new List<string>()
                    ))
                .ToListAsync();
            return ResultList<TareaDto>.Success(entities);
        }
        catch (Exception Ex)
        {
            return ResultList<TareaDto>.Failure($"Error: {Ex.Message}");
        }
    }
    public async Task<ResultList<TareaDto>> ObtenerTareasPorColaborador(string email)
    {
        try
        {
            var tareas = await dbContext.Tareas
     .Include(t => t.Colaboradores) // Asegúrate de cargar los colaboradores
     .Where(t => t.Colaboradores!.Any(c => c.CreadorEmail == email || c.ColaboradorEmail == email))
     .Select(t => new TareaDto(
         t.Id,
         t.UserId,
         t.Titulo,
         t.Descripcion!,
         t.Estado,
         t.Prioridad,
         t.FechaCreacion,
         t.FechaLimite,
         t.IsCompleted,
          string.Empty,
          new List<string>()
     ))
     .ToListAsync();

            if (!tareas.Any())
                return ResultList<TareaDto>.Failure("No se encontraron tareas asociadas a este colaborador.");

            return ResultList<TareaDto>.Success(tareas);
        }
        catch (Exception ex)
        {
            return ResultList<TareaDto>.Failure($"☠️ Error: {ex.Message}");
        }
    }
}
