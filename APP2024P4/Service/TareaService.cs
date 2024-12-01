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
                tarea.Descripcion,
                tarea.ColaboradorId
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
                tarea.Descripcion,
                tarea.ColaboradorId
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
                .Select(p => new TareaDto(
                    p.Id,
                    p.UserId,
                    p.Titulo,
                    p.Descripcion!,
                    p.Estado,
                    p.Prioridad,
                    p.ColaboradorId,
                    p.Colaboradores!.CreadorEmail ?? "Sin colaborador todavia",
                    p.FechaCreacion,
                    p.FechaLimite,
                    p.IsCompleted
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
                .Select(p => new TareaDto(
                    p.Id,
                    p.UserId,
                    p.Titulo,
                    p.Descripcion!,
                    p.Estado,
                    p.Prioridad,
                    p.ColaboradorId,
                    p.Colaboradores!.CreadorEmail ?? "Sin colaborador todavia",
                    p.FechaCreacion,
                    p.FechaLimite,
                    p.IsCompleted
                    ))
                .ToListAsync();
            return ResultList<TareaDto>.Success(entities);
        }
        catch (Exception Ex)
        {
            return ResultList<TareaDto>.Failure($"Error: {Ex.Message}");
        }
    }
}
