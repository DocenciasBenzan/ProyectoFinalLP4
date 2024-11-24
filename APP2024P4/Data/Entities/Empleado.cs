using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APP2024P4.Data.Entities;


public class Empleado
{
	[Key]
	public int Id { get; set; }
	public string Nombre { get; set; } = null!;
	public string Apellido { get; set; } = null!;
	public string Telefono { get; set; } = null!;
	public string CorreoElectronico { get; set; } = null!;
	public DateTime InicioTrabajo { get; set; }
	public DateTime FinTrabajo { get; set; }
	public bool Activo { get; set; }
}
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
public class Vehiculo
{

	[Key]
	public int Id { get; set; }

	public string Placa { get; set; } = null!;
	public string Marca { get; set; } = null!;
	public string Modelo { get; set; } = null!;
	public string Color { get; set; } = null!;
	public string Tipo { get; set; } = null!; // sedán, SUV, camioneta

}
public class Cliente
{
	[Key]
	public int Id { get; set; }

	public string Nombre { get; set; } = null!;
	public string Telefono { get; set; } = null!;
	public string? CorreoElectronico { get; set; } = null!;
	public string? Direcion { get; set; }
}
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

	#region Foreign Keys
	[ForeignKey(nameof(ClienteId))]
	public virtual Cliente Cliente { get; set; } = null!;

	[ForeignKey(nameof(VehiculoId))]
	public virtual Vehiculo Vehiculo { get; set; } = null!;

	[ForeignKey(nameof(ServicioId))]
	public virtual Servicio Servicio { get; set; } = null!;

	[ForeignKey(nameof(EmpleadoId))]
	public virtual Empleado Empleado { get; set; } = null!;
	#endregion
}
