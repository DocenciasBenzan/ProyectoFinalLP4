using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APP2024P4.Data.Entities;

public class Reserva
{
	[Key]
	public int Id { get; set; }
	public DateTime Inicio { get; set; }
	public DateTime Fin { get; set; }
	public int ClienteId { get; set; }
	public int VehiculoId { get; set; }
	public int ServicioId { get; set; }
	public int EmpleadoId { get; set; }
	public string Estado { get; set; } = null!; // pendiente, completada, cancelada
	public string? NotasAdicionales { get; set; }
	#region Foreign Keys
	[ForeignKey(nameof(ClienteId))]
	public virtual Cliente Cliente { get; set; } = null!;
	[ForeignKey(nameof(ServicioId))]
	public virtual Servicio Servicio { get; set; } = null!;
	[ForeignKey(nameof(EmpleadoId))]
	public virtual Empleado Empleado { get; set; } = null!;
	#endregion
}