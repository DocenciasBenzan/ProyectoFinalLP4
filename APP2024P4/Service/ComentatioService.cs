using APP2024P4.Data;
using APP2024P4.Data.Dtos;
using APP2024P4.Data.Entidades;
using Microsoft.EntityFrameworkCore;
using TaskMaster;

namespace APP2024P4.Service;

public interface IComentatioService
{

    Task<Result> Create(ComentarioRequest comentario, string userId, string creadorEmail);
    Task<Result> Update(ComentarioRequest comentario);
    Task<Result> Delete(int id);
    Task<ResultList<ComentarioDto>> ConsultarComentario(int TareaId);
}
public partial class ComentatioService : IComentatioService
{
    private readonly IApplicationDbContext _dbContext;

    public ComentatioService(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    // Crea un nuevo comentario
    public async Task<Result> Create(ComentarioRequest comentario, string userId, string creadorEmail)
    {
        try
        {
            var entity = Comentario.Create(
                comentario.Contenido,
                comentario.UserId,
                comentario.CreadorEmail,
                comentario.TareaId,
                comentario.FechaCreacion,
                comentario.FechaActualizacion
            );
            entity.UserId = userId;
            entity.CreadorEmail = creadorEmail;

            _dbContext.Comentarios.Add(entity);
            await _dbContext.SaveChangesAsync();

            return Result.Success("Comentario registrado con éxito!");
        }
        catch (Exception ex)
        {
            return Result.Failure($"Error: {ex.Message}");
        }
    }

    // Actualiza un comentario existente
    public async Task<Result> Update(ComentarioRequest comentario)
    {
        try
        {
            var entity = _dbContext.Comentarios.FirstOrDefault(p => p.Id == comentario.Id);
            if (entity == null)
                return Result.Failure($"El comentario '{comentario.Id}' no existe!");

            if (entity.Update(
                comentario.Contenido,
                comentario.UserId,
                comentario.CreadorEmail,
                comentario.TareaId,
                comentario.FechaCreacion,
                comentario.FechaActualizacion))
            {
                await _dbContext.SaveChangesAsync();
                return Result.Success("Comentario actualizado con éxito!");
            }

            return Result.Failure("No se realizaron cambios.");
        }
        catch (Exception ex)
        {
            return Result.Failure($"Error: {ex.Message}");
        }
    }

    // Elimina un comentario por su ID
    public async Task<Result> Delete(int id)
    {
        try
        {
            var entity = _dbContext.Comentarios.FirstOrDefault(p => p.Id == id);
            if (entity == null)
                return Result.Failure($"El comentario '{id}' no existe!");

            _dbContext.Comentarios.Remove(entity);
            await _dbContext.SaveChangesAsync();

            return Result.Success("Comentario eliminado con éxito!");
        }
        catch (Exception ex)
        {
            return Result.Failure($"Error: {ex.Message}");
        }
    }

    // Consulta los comentarios de una tarea
    public async Task<ResultList<ComentarioDto>> ConsultarComentario(int TareaId)
    {
        try
        {
            var comentarios = await _dbContext.Comentarios
                .Where(c => c.TareaId == TareaId)
                .Select(c => new ComentarioDto(
                    c.Id,
                    c.Contenido,
                    c.UserId,
                    c.CreadorEmail,
                    c.TareaId,
                    c.FechaCreacion,
                    c.FechaActualizacion
                ))
               .ToListAsync();

            if (!comentarios.Any())
                return ResultList<ComentarioDto>.Failure("No se encontraron comentarios para esta tarea.");

            return ResultList<ComentarioDto>.Success(comentarios);
        }
        catch (Exception ex)
        {
            return ResultList<ComentarioDto>.Failure($"Error: {ex.Message}");
        }
    }
}
