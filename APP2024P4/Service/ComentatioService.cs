using APP2024P4.Data;
using APP2024P4.Data.Dtos;
using APP2024P4.Data.Entidades;
using TaskMaster;

namespace APP2024P4.Service
{
    public interface IComentatioService
    {

    }
    public partial class ComentatioService : IComentatioService
    {
        private readonly IApplicationDbContext _dbContext;

        public ComentatioService(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Result> Create(ComentarioRequest comentario, string userId)
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
                    return Result.Failure($"La Tarea '{comentario.Id}' no existe!");

                if (entity.Update(
                    comentario.Contenido,
                    comentario.UserId,
                    comentario.CreadorEmail,
                    comentario.TareaId,
                    comentario.FechaCreacion,
                    comentario.FechaActualizacion
                    ))
                {
                    await _dbContext.SaveChangesAsync();
                    return Result.Success("Comentario actualizada con éxito!");
                }
                return Result.Success("No se realizaron cambios.");
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

    }
}
