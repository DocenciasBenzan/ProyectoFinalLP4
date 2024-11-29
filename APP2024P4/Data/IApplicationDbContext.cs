using APP2024P4.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace APP2024P4.Data
{
	public interface IApplicationDbContext
	{
		DbSet<Cliente> Clientes { get; set; }
		DbSet<Empleado> Empleados { get; set; }
		DbSet<Reserva> Reservas { get; set; }
		DbSet<Servicio> Servicios { get; set; }
		DbSet<Vehiculo> Vehiculos { get; set; }

		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
	}
}