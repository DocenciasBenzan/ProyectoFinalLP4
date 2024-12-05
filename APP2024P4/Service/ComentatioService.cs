using APP2024P4.Data;
using APP2024P4.Data.Dtos;
using APP2024P4.Data.Entidades;
using Microsoft.EntityFrameworkCore;
using TaskMaster;

namespace APP2024P4.Service
{
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
        public async Task<Result> Create(ComentarioRequest comentario, string userId,string creadorEmail)
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

                return Result.Success("Comentario registrada con éxito!");
            }
            catch (Exception ex)
            {
                return Result.Failure($"Error: {ex.Message}");
            }
        }

        public async Task<Result> Update(ComentarioRequest comentario)
        {
            try
            {
                var entity = _dbContext.Comentarios.FirstOrDefault(p => p.Id == comentario.Id);
                if (entity == null)
                    return Result.Failure($"el Cometario '{comentario.Id}' no existe!");

                if (entity.Update(
                    comentario.Contenido,
                    comentario.UserId,
                    comentario.CreadorEmail,
                    comentario.TareaId,
                    comentario.FechaCreacion,
                    comentario.FechaActualizacion
                    ))
                    return Result.Success("Comentario actualizada con éxito!");
                {
                    await _dbContext.SaveChangesAsync();
                    return Result.Success("Comentario actualizada con éxito!");
                }
            }
            catch (Exception ex)
            {
                return Result.Failure($"Error: {ex.Message}");
            }
        }

        public async Task<Result> Delete(int id)
        {
            try
            {
                var entity = _dbContext.Comentarios.FirstOrDefault(p => p.Id == id);
                if (entity == null)
                    return Result.Failure($"El Comentario '{id}' no existe!");

                _dbContext.Comentarios.Remove(entity);
                await _dbContext.SaveChangesAsync();

                return Result.Success("Comentario eliminada con éxito!");
            }
            catch (Exception ex)
            {
                return Result.Failure($"Error: {ex.Message}");
            }
        }

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
                    return ResultList<ComentarioDto>.Failure("No se encontraron tareas asociadas a este Id.");

                return ResultList<ComentarioDto>.Success(comentarios);
            }
            catch (Exception ex)
            {
                return ResultList<ComentarioDto>.Failure($"Error: {ex.Message}");
            }
        }

    }
}
