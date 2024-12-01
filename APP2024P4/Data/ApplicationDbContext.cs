using APP2024P4.Data.Entidades;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace APP2024P4.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options), IApplicationDbContext
    {
        public DbSet<Tarea> Tareas { get; set; }
        public DbSet<Colaborador> Colaboradores { get; set; }
        public DbSet<Notificacion> Notificaciones { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
