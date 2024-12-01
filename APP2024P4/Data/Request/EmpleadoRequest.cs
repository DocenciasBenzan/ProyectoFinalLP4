

using System.ComponentModel.DataAnnotations;
using APP2024P4.Data.Entities;

namespace APP2024P4.Data.Request;

public class EmpleadoRequest
{
	public int Id { get; set; }
	[Required(ErrorMessage = "El nombre es obligatorio.")]
	[MaxLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres.")]
	public string Nombre { get; set; } = null!;
	[Required(AllowEmptyStrings = false, ErrorMessage = "El apellido es obligatorio.")]
	[MaxLength(50, ErrorMessage = "El apellido no puede exceder los 50 caracteres.")]
	public string Apellido { get; set; } = null!;
	[Required(ErrorMessage = "El teléfono es obligatorio.")]
	[Phone(ErrorMessage = "El formato del teléfono no es válido.")]
	public string Telefono { get; set; } = null!;
	[Required(ErrorMessage = "El correo electrónico es obligatorio.")]
	[EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
	public string CorreoElectronico { get; set; } = null!;
	[Required(ErrorMessage = "La fecha de inicio de trabajo es obligatoria.")]
	public DateTime InicioTrabajo { get; set; }
	[Required(ErrorMessage = "La fecha de fin de trabajo es obligatoria.")]
	public DateTime FinTrabajo { get; set; }
	public bool Activo { get; set; }
	public Empleado ToEmpleado()
	{
		return new Empleado()
		{
			Id = this.Id,
			Nombre = this.Nombre,
			Apellido = this.Apellido,
			Telefono = this.Telefono,
			CorreoElectronico = this.CorreoElectronico,
			InicioTrabajo = this.InicioTrabajo,
			FinTrabajo = this.FinTrabajo,
			Activo = this.Activo
		};
	}
}
public class ServicioRequest
{
	public string Nombre { get; set; } = null!;
	public string Descripcion { get; set; } = null!;
	public decimal DuracionEstimada { get; set; }
	public decimal Precio { get; set; }
}
public class VehiculoRequest
{
	public int Id { get; set; }
	[Required(ErrorMessage = "La placa es obligatoria.")]
	[StringLength(10, ErrorMessage = "La placa no puede tener más de 10 caracteres.")]
	public string Placa { get; set; } = null!;

	[Required(ErrorMessage = "La marca es obligatoria.")]
	[StringLength(50, ErrorMessage = "La marca no puede tener más de 50 caracteres.")]
	public string Marca { get; set; } = null!;

	[Required(ErrorMessage = "El modelo es obligatorio.")]
	[StringLength(50, ErrorMessage = "El modelo no puede tener más de 50 caracteres.")]
	public string Modelo { get; set; } = null!;

	[Required(ErrorMessage = "El color es obligatorio.")]
	[StringLength(20, ErrorMessage = "El color no puede tener más de 20 caracteres.")]
	public string Color { get; set; } = null!;

	[Required(ErrorMessage = "El tipo es obligatorio.")]
	[StringLength(30, ErrorMessage = "El tipo no puede tener más de 30 caracteres.")]
	public string Tipo { get; set; } = null!;
	public int ClienteId { get; set; }

	public Vehiculo ToVehiculo()
	{
		return new Vehiculo()
		{
			Id = this.Id,
			Placa = this.Placa,
			Marca = this.Marca,
			Modelo = this.Modelo,
			Color = this.Color,
			Tipo = this.Tipo,
			ClientId = this.ClienteId
		};
	}
}
public class ClienteRequest
{
	public string Nombre { get; set; } = null!;
	public string Telefono { get; set; } = null!;
	public string? CorreoElectronico { get; set; }
	public string? Direcion { get; set; }
}

public class ReservaRequest
{
	public DateTime Inicio { get; set; }
	public DateTime Fin { get; set; }
	public int ClienteId { get; set; }
	public int VehiculoId { get; set; }
	public int ServicioId { get; set; }
	public int EmpleadoId { get; set; }
	public string Estado { get; set; } = null!; // pendiente, completada, cancelada
	public string? NotasAdicionales { get; set; }
}
