using Microsoft.EntityFrameworkCore;
using APP2024P4.Data;
using APP2024P4.Data.Dtos;
using APP2024P4.Data.Entities;

namespace APP2024P4.Services
{
    public interface ICategoriaService
    {
        Task<Result> Create(CategoriaRequest request);
        Task<Result> Delete(int Id);
        Task<ResultList<CategoriaDto>> GetAll(CancellationToken cancellationToken = default);
        Task<Result<CategoriaDto>> GetById(int Id);
        Task<Result> Update(CategoriaRequest request);
    }
    public class CategoriaService(IApplicationDbContext dbcontext) : ICategoriaService
    {
        public async Task<ResultList<CategoriaDto>> GetAll(CancellationToken cancellationToken = default)
        {
            var categorias = await dbcontext.Categorias
                .Select(x => x.ToDto())
                .ToListAsync(cancellationToken);
            return ResultList<CategoriaDto>.Success(categorias);
        }
        public async Task<Result> Create(CategoriaRequest request)
        {
            try
            {
                var entity = Categoria.Create(request.Nombre);
                dbcontext.Categorias.Add(entity);
                await dbcontext.SaveChangesAsync();
                return Result.Success("Categoria registrada satisfactoriamente");
            }
            catch (Exception VariableParaCapturarError)
            {
                return Result.Failure($"Error: {VariableParaCapturarError.Message}");
            }
        }
        public async Task<Result> Update(CategoriaRequest categoria)
        {
            try
            {
                var entity = dbcontext.Categorias.Where(p => p.Id == categoria.Id).FirstOrDefault();
                if (entity == null)
                    return Result.Failure($"La categoria '{categoria.Id}' no existe!");
                if (entity.Update(categoria.Nombre))
                {
                    await dbcontext.SaveChangesAsync();
                    return Result.Success("✅Categoria modificado con exito!");
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
                var entity = dbcontext.Categorias.Where(p => p.Id == Id).FirstOrDefault();
                if (entity == null)
                    return Result.Failure($"La categoría '{Id}' no existe!");
                dbcontext.Categorias.Remove(entity);
                await dbcontext.SaveChangesAsync();
                return Result.Success("✅Categoría eliminada con exito!");
            }
            catch (Exception VariableParaCapturarErrores)
            {
                return Result.Failure($"☠️ Error: {VariableParaCapturarErrores.Message}");
            }
        }
        public async Task<Result<CategoriaDto>> GetById(int Id)
        {
            try
            {
                var entity = await dbcontext.Categorias.Where(p => p.Id == Id)
                    .Select(p => new CategoriaDto (p.Id, p.Nombre ))
                    .FirstOrDefaultAsync();
                if (entity == null)
                    return Result<CategoriaDto>.Failure($"El producto '{Id}' no existe!");
                return Result<CategoriaDto>.Success(entity);
            }
            catch (Exception VariableParaCapturarErrores)
            {
                return Result<CategoriaDto>.Failure($"☠️ Error: {VariableParaCapturarErrores.Message}");
            }
        }
        public async Task<ResultList<CategoriaDto>> Get(string filtro = "")
        {
            try
            {
                var entities = await dbcontext.Categorias
                    .Where(p => p.Nombre.ToLower().Contains(filtro.ToLower()))
                    .Select(p => new CategoriaDto(p.Id, p.Nombre))
                    .ToListAsync();
                return ResultList<CategoriaDto>.Success(entities);
            }
            catch (Exception VariableParaCapturarErrores)
            {
                return ResultList<CategoriaDto>.Failure($"☠️ Error: {VariableParaCapturarErrores.Message}");
            }
        }
    }
}
