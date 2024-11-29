using APP2024P4.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace APP2024P4.Data
{
	public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options), IApplicationDbContext
	{
		public DbSet<Empleado> Empleados { get; set; }
		public DbSet<Servicio> Servicios { get; set; }
		public DbSet<Vehiculo> Vehiculos { get; set; }
		public DbSet<Cliente> Clientes { get; set; }
		public DbSet<Reserva> Reservas { get; set; }

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			return base.SaveChangesAsync(cancellationToken);
		}
	}
}
