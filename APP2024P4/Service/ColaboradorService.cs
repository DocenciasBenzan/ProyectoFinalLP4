using APP2024P4.Data.Dtos;
using APP2024P4.Data.Entidades;
using APP2024P4.Data;
using TaskMaster;
using Microsoft.EntityFrameworkCore;
namespace APP2024P4.Service;

public interface IColaboradorService
{
    Task<Result> Addcolaborador(ColaboradorRequest colaborador);
    Task<Result> Update(ColaboradorRequest colaborador);
    Task<Result> Delete(int Id);
    Task<ResultList<ColaboradorDto>> GetByEmail(string creadorEmail);
}
public partial class ColaboradorService : IColaboradorService
{

    private readonly IApplicationDbContext _context;

    public ColaboradorService(ApplicationDbContext _context)
    {
        this._context = _context;
    }

    // Agrega un nuevo colaborador
    public async Task<Result> Addcolaborador(ColaboradorRequest colaborador)
    {
        try
        {
            var entity = Colaborador.Create(
                colaborador.CreadorEmail,
                colaborador.ColaboradorEmail,
                colaborador.IsApproved,
                colaborador.TareaId,
                colaborador.IsCompleted,
                colaborador.UserId
            );
            _context.Colaboradores.Add(entity);
            await _context.SaveChangesAsync();

            return Result.Success("Colaborador registrado con éxito!");
        }
        catch (Exception Ex)
        {
            return Result.Failure($"Error: {Ex.Message}");
        }
    }

    // Actualiza un colaborador existente
    public async Task<Result> Update(ColaboradorRequest colaborador)
    {
        try
        {
            var entity = _context.Colaboradores.FirstOrDefault(p => p.Id == colaborador.Id);
            if (entity == null)
                return Result.Failure($"El colaborador '{colaborador.Id}' no existe!");

            if (entity.Update(
                colaborador.UserId,
                colaborador.CreadorEmail,
                colaborador.IsCompleted,
                colaborador.TareaId,
                colaborador.ColaboradorEmail,
                colaborador.IsApproved
            ))
            {
                await _context.SaveChangesAsync();
                return Result.Success("Colaborador modificado con éxito!");
            }

            return Result.Success("No se realizaron cambios.");
        }
        catch (Exception Ex)
        {
            return Result.Failure($"Error: {Ex.Message}");
        }
    }

    // Elimina un colaborador por su ID
    public async Task<Result> Delete(int Id)
    {
        try
        {
            var entity = _context.Colaboradores.FirstOrDefault(p => p.Id == Id);
            if (entity == null)
                return Result.Failure($"El colaborador '{Id}' no existe!");

            _context.Colaboradores.Remove(entity);
            await _context.SaveChangesAsync();

            return Result.Success("Colaborador eliminado con éxito!");
        }
        catch (Exception Ex)
        {
            return Result.Failure($"Error: {Ex.Message}");
        }
    }

    // Obtiene colaboradores por el correo del creador
    public async Task<ResultList<ColaboradorDto>> GetByEmail(string creadorEmail)
    {
        try
        {
            var entity = await _context.Colaboradores
                .Where(p => p.CreadorEmail == creadorEmail)
                .Select(c => new ColaboradorDto(
                    c.Id,
                    c.UserId,
                    c.TareaId,
                    c.CreadorEmail,
                    c.ColaboradorEmail,
                    c.IsApproved,
                    c.IsCompleted
                ))
                .ToListAsync();

            if (!entity.Any())
                return ResultList<ColaboradorDto>.Failure("No se encontraron colaboradores para este correo.");

            return ResultList<ColaboradorDto>.Success(entity);
        }
        catch (Exception Ex)
        {
            return ResultList<ColaboradorDto>.Failure($"Error: {Ex.Message}");
        }
    }
}