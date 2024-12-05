using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using APP2024P4.Data.Response;

namespace APP2024P4.Data.Entities;

public class FacturaParte
{
	[Key]
	public int Id { get; set; }
	public int FacturaID { get; set; }
	[ForeignKey(nameof(FacturaID))]
	public Factura Factura { get; set; }

	public int PiezaId { get; set; }
	[ForeignKey(nameof(PiezaId))]
	public Pieza Pieza { get; set; }

	[Required]
	public int Cantidad { get; set; }

	public FacturaParteResponse ToResponse()
	{
		return new FacturaParteResponse
		{
			Id = this.Id,
			FacturaID = this.FacturaID,
			factura = this.Factura.ToResponse(),
			PiezaId = this.PiezaId,
			pieza = this.Pieza?.ToResponse(),
			Cantidad = this.Cantidad
		};
	}
}