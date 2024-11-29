using APP2024P4.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace APP2024P4.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options), IApplicationDbContext
{
	public DbSet<Pieza> Piezas { get; set; }
	public DbSet<Factura> Facturas { get; set; }
	public DbSet<FacturaParte> FacturaPartes { get; set; }
	public DbSet<Cliente> Clientes { get; set; }
}
