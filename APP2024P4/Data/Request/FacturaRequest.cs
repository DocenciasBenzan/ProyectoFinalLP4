using System.ComponentModel.DataAnnotations;

namespace APP2024P4.Data.Request;

public class FacturaRequest
{
	public int FacturaID { get; set; }
	[Required(ErrorMessage = "La fecha es obligatoria.")]
	public DateTime Fecha { get; set; }
	public decimal Total { get; set; }
	[Required(ErrorMessage = "Las items son obligatorias.")]
	public List<FacturaParteRequest> FacturaPartes { get; set; } = new List<FacturaParteRequest>();
	[Required(ErrorMessage = "El cliente es obligatorio.")]
	public ClienteRequest Cliente { get; set; }
}

