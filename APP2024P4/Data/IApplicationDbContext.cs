using APP2024P4.Data.Entidades;
using Microsoft.EntityFrameworkCore;

namespace APP2024P4.Data;
public interface IApplicationDbContext
{
    DbSet<Tarea> Tareas { get; set; }
    DbSet<Colaborador> Colaboradores { get; set; }
    DbSet<Notificacion> Notificaciones { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);


}
