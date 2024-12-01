using System.ComponentModel.DataAnnotations;

namespace APP2024P4.Data.Entities;

public class Servicio
{
	[Key]
	public int Id { get; set; }
	public string Nombre { get; set; } = null!;
	public string Descripcion { get; set; } = null!;
	// pasado a hora
	public decimal DuracionEstimada { get; set; }
	public decimal Precio { get; set; }
}
