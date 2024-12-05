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
	public List<FacturaParte> FacturaPartes { get; set; } = new();
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
//public FacturaResponse ToResponse()
//{

//	return new FacturaResponse
//	{
//		FacturaID = FacturaID,
//		Fecha = Fecha,
//		Total = Total,
//		Cliente = new ClienteResponse
//		{
//			Id = Cliente.Id,
//			Nombre = Cliente.Nombre
//		},
//		FacturaPartes = FacturaPartes.Select(fp => new FacturaParteResponse
//		{
//			PiezaId = fp.Pieza.Id,
//			pieza = fp.Pieza.ToResponse(),
//			Cantidad = fp.Cantidad,
//			factura = fp.Factura.ToResponse(),
//			FacturaID = fp.FacturaID
//		}).ToList()
//	};
//}
//}
