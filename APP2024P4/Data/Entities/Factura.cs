using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using APP2024P4.Data.Response;

namespace APP2024P4.Data.Entities;

public class Factura
{
	[Key]
	public int FacturaID { get; set; }

	[Required]
	public DateTime Fecha { get; set; }

	[Required]
	[Column(TypeName = "decimal(10, 2)")]
	public decimal Total { get; set; }

	public int ClienteId { get; set; }
	[ForeignKey(nameof(ClienteId))]
	public Cliente Cliente { get; set; }
	public virtual List<FacturaParte> FacturaPartes { get; set; } = new();
	public FacturaResponse ToResponse()
	{
		return new FacturaResponse
		{
			FacturaID = this.FacturaID,
			Fecha = this.Fecha,
			Cliente = new ClienteResponse()
			{
				Id = this.Cliente.Id,
				Nombre = this.Cliente.Nombre
			},
			FacturaPartes = this.FacturaPartes?.Select(fp => fp.ToResponse()).ToList() ?? new List<FacturaParteResponse>()
		};
	}
}

