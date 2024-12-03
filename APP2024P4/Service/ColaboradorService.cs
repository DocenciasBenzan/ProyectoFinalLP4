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

    public async Task<Result> Addcolaborador(ColaboradorRequest colaborador)
    {
        try
        {
            var entity = Colaborador.Create(
                colaborador.CreadorEmail,
                colaborador.ColaboradorEmail,
                colaborador.IsApproved,
                colaborador.TareaId,
                colaborador.UserId
                );
            _context.Colaboradores.Add(entity);
            await _context.SaveChangesAsync();

            return Result.Success("✅Notificaion registrado con éxito!");

        }
        catch (Exception Ex)
        {
            return Result.Failure($"☠️ Error: {Ex.Message}");
        }
    }

    public async Task<Result> Update(ColaboradorRequest colaborador)
    {
        try
        {
            var entity = _context.Colaboradores.Where(p => p.Id == colaborador.Id).FirstOrDefault();
            if (entity == null)
                return Result.Failure($"El producto '{colaborador.Id}' no existe!");
            if (entity.Update(
                colaborador.UserId,
                colaborador.CreadorEmail,
                colaborador.TareaId,
                colaborador.ColaboradorEmail,
                colaborador.IsApproved
                ))
            {
                await _context.SaveChangesAsync();
                return Result.Success("✅Producto modificado con exito!");
            }
            return Result.Success("🐫 No has realizado ningun cambio!");
        }
        catch (Exception Ex)
        {
            return Result.Failure($"☠️ Error: {Ex.Message}");
        }
    }

    public async Task<Result> Delete(int Id)
    {
        try
        {
            var entity = _context.Colaboradores.Where(p => p.Id == Id).FirstOrDefault();
            if (entity == null)
                return Result.Failure($"La categoría '{Id}' no existe!");
            _context.Colaboradores.Remove(entity);
            await _context.SaveChangesAsync();
            return Result.Success("✅Categoría eliminada con exito!");
        }
        catch (Exception Ex)
        {
            return Result.Failure($"☠️ Error: {Ex.Message}");
        }
    }

    public async Task<ResultList<ColaboradorDto>> GetByEmail(string creadorEmail)
    {
        try
        {

            var entity = await _context.Colaboradores.Where(p => p.CreadorEmail == creadorEmail)

                .Select(c => new ColaboradorDto(
                    c.Id,
                    c.UserId,
                    c.TareaId,
                    c.CreadorEmail,
                    c.ColaboradorEmail,
                    c.IsApproved
                    ))
                .ToListAsync();
            if (entity == null)
                return ResultList<ColaboradorDto>.Failure($"El producto no existe!");

            return ResultList<ColaboradorDto>.Success(entity);
        }
        catch (Exception Ex)
        {
            return ResultList<ColaboradorDto>.Failure($"☠️ Error: {Ex.Message}");
        }
    }

}