using APP2024P4.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace APP2024P4.Data
{
	public interface IApplicationDbContext
	{
		DbSet<Cliente> Clientes { get; set; }
		DbSet<FacturaParte> FacturaPartes { get; set; }
		DbSet<Factura> Facturas { get; set; }
		DbSet<Pieza> Piezas { get; set; }
	}
}