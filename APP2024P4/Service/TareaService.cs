using APP2024P4.Data;
using APP2024P4.Data.Dtos;
using APP2024P4.Data.Entidades;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskMaster;

namespace APP2024P4.Service;

public interface ITareaService
{
    Task<Result> Create(TareaRequest tarea, string userId);
    Task<Result> Delete(int id);
    Task<ResultList<TareaDto>> Get(string filtro = "");
    Task<ResultList<TareaDto>> GetById(string userId);
    Task<Result> Update(TareaRequest tarea);
    Task<ResultList<TareaDto>> ObtenerTareasPorColaborador(string email);
}

public partial class TareaService : ITareaService
{
    private readonly IApplicationDbContext _dbContext;

    public TareaService(IApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
    {
        _dbContext = dbContext;
    }

    // Crea una nueva tarea
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

            _dbContext.Tareas.Add(entity);
            await _dbContext.SaveChangesAsync();

            return Result.Success("Tarea registrada con éxito!");
        }
        catch (Exception ex)
        {
            return Result.Failure($"Error: {ex.Message}");
        }
    }

    // Actualiza una tarea existente
    public async Task<Result> Update(TareaRequest tarea)
    {
        try
        {
            var entity = _dbContext.Tareas.FirstOrDefault(p => p.Id == tarea.Id);
            if (entity == null)
                return Result.Failure($"La tarea '{tarea.Id}' no existe!");

            if (entity.Update(
                tarea.Titulo,
                tarea.FechaCreacion,
                tarea.FechaLimite,
                tarea.IsCompleted,
                tarea.Estado,
                tarea.Prioridad,
                tarea.Descripcion))
            {
                await _dbContext.SaveChangesAsync();
                return Result.Success("Tarea actualizada con éxito!");
            }

            return Result.Success("No se realizaron cambios.");
        }
        catch (Exception ex)
        {
            return Result.Failure($"Error: {ex.Message}");
        }
    }

    // Elimina una tarea por ID
    public async Task<Result> Delete(int id)
    {
        try
        {
            var entity = _dbContext.Tareas.FirstOrDefault(p => p.Id == id);
            if (entity == null)
                return Result.Failure($"La tarea '{id}' no existe!");

            _dbContext.Tareas.Remove(entity);
            await _dbContext.SaveChangesAsync();

            return Result.Success("Tarea eliminada con éxito!");
        }
        catch (Exception ex)
        {
            return Result.Failure($"Error: {ex.Message}");
        }
    }

    // Obtiene las tareas de un usuario por ID
    public async Task<ResultList<TareaDto>> GetById(string userId)
    {
        try
        {
            var tareas = await _dbContext.Tareas
                .Where(p => p.UserId == userId)
                .Include(t => t.Colaboradores)
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
                    t.Colaboradores!.Select(c => new ColaboradorDto(
                        c.Id,
                        c.UserId,
                        c.TareaId,
                        c.CreadorEmail,
                        c.ColaboradorEmail,
                        c.IsApproved,
                        c.IsCompleted
                    )).ToList()
                ))
                .ToListAsync();

            return ResultList<TareaDto>.Success(tareas);
        }
        catch (Exception ex)
        {
            return ResultList<TareaDto>.Failure($"Error: {ex.Message}");
        }
    }

    // Filtra tareas por título
    public async Task<ResultList<TareaDto>> Get(string filtro = "")
    {
        try
        {
            var tareas = await _dbContext.Tareas
                .Where(p => p.Titulo.ToLower().Contains(filtro.ToLower()))
                .Include(t => t.Colaboradores)
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
                    t.Colaboradores!.Select(c => new ColaboradorDto(
                        c.Id,
                        c.UserId,
                        c.TareaId,
                        c.CreadorEmail,
                        c.ColaboradorEmail,
                        c.IsApproved,
                        c.IsCompleted
                    )).ToList()
                ))
                .ToListAsync();

            return ResultList<TareaDto>.Success(tareas);
        }
        catch (Exception ex)
        {
            return ResultList<TareaDto>.Failure($"Error: {ex.Message}");
        }
    }

    // Obtiene tareas asociadas a un colaborador por su email
    public async Task<ResultList<TareaDto>> ObtenerTareasPorColaborador(string email)
    {
        try
        {
            var tareas = await _dbContext.Tareas
                .Include(t => t.Colaboradores)
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
                    t.Colaboradores!.Select(c => new ColaboradorDto(
                        c.Id,
                        c.UserId,
                        c.TareaId,
                        c.CreadorEmail,
                        c.ColaboradorEmail,
                        c.IsApproved,
                        c.IsCompleted
                    )).ToList()
                ))
                .ToListAsync();

            if (!tareas.Any())
                return ResultList<TareaDto>.Failure("No se encontraron tareas asociadas a este colaborador.");

            return ResultList<TareaDto>.Success(tareas);
        }
        catch (Exception ex)
        {
            return ResultList<TareaDto>.Failure($"Error: {ex.Message}");
        }
    }
}
