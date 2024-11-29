using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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
}
public class Factura
{
	[Key]
	public int FacturaID { get; set; } 

	[Required]
	public DateTime Fecha { get; set; } 

	[Required]
	[Column(TypeName = "decimal(10, 2)")]
	public decimal Total { get; set; }
	public List<FacturaParte> FacturaPartes { get; set; } = new();
    public int ClienteId { get; set; }
	[ForeignKey(nameof(ClienteId))]
    public Cliente Cliente { get; set; }
}
public class Pieza
{
	[Key]
	public int Id { get; set; }

	[Required]
	[MaxLength(100)]
	public string Nombre { get; set; } = null!;

	[Required]
	[Column(TypeName = "decimal(10, 2)")]
	public decimal Precio { get; set; }

	public string Imagen { get; set; } = null!;
    public string Marca { get; set; } = null!;

	[Range(0,int.MaxValue,ErrorMessage ="Cantidad debe ser al menos 0")]
    public int CantidadDisponible { get; set; }
    public List<FacturaParte> FacturaPartes { get; set; } = new();
}
public class Cliente{
    public int Id { get; set; }
	public string Nombre { get; set; } = null!;
}