using APP2024P4.Data;
using APP2024P4.Data.Request;
using APP2024P4.Data.Response;
using Microsoft.EntityFrameworkCore;

namespace APP2024P4.Servicios;

public interface IEmpleadoServicio
{
    Task<Result> AgregarEmpleado(EmpleadoRequest request);
    Task<Result> EliminarEmpleado(int EmpleadoId);
	Task<ResultList<EmpleadoResponse>> ObtenerEmpleados(string filter = "");
}

public class EmpleadoServicio(IApplicationDbContext context) : IEmpleadoServicio
{
    public async Task<ResultList<EmpleadoResponse>> ObtenerEmpleados(string filter = "")
    {
        try
        {
            var r = await context.Empleados.Select(
            x => x.ToResponse()).AsNoTracking().ToListAsync();
             return ResultList<EmpleadoResponse>.Success(r);
            
        }
        catch (Exception ex)
        {
            return ResultList<EmpleadoResponse>.Failure($"Error cargando los empleados: ${ex.Message}");
        }
    }
    public async Task<Result> AgregarEmpleado(EmpleadoRequest request)
    {
        try
        {
            var empleado = request.ToEmpleado();
            context.Empleados.Add(empleado);
            await context.SaveChangesAsync().ConfigureAwait(false);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure($"Error al agregar el empleado : {ex}");
        }
    }

    public async Task<Result> EliminarEmpleado(int EmpleadoId)
    {
        try
        {
            var empleado =  context.Empleados.FirstOrDefault(x => x.Id == EmpleadoId);
            if (empleado is not null)
            {
                context.Empleados.Remove(empleado);
                await context.SaveChangesAsync();
                return Result.Success();
            }
            return Result.Failure("Empleado no encontrado");
        }
        catch (Exception ex)
        {
            return Result.Failure($"Error al agregar el empleado : {ex}");
        }
    }
}


