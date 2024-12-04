

using System.ComponentModel.DataAnnotations;
using APP2024P4.Data.Entities;
using APP2024P4.Data.Response;

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
	public int Id { get; set; }
	[Required(AllowEmptyStrings = false, ErrorMessage = "Ingresa un Nombre")]
	public string Nombre { get; set; } = null!;
	[Required(AllowEmptyStrings = false, ErrorMessage = "Ingresa una Descripcion")]

	public string Descripcion { get; set; } = null!;
	[Range(1, double.MaxValue, ErrorMessage = "Ingresa una duracion correcta ( >  )")]
	public decimal DuracionEstimada { get; set; }
	[Range(1, double.MaxValue, ErrorMessage = "Ingresa un precio ( > 1 )")]

	public decimal Precio { get; set; }

	public Servicio ToServicio()
	{
		return new Servicio()
		{
			Id = this.Id,
			Nombre = this.Nombre,
			Descripcion = this.Descripcion,
			DuracionEstimada = this.DuracionEstimada,
			Precio = this.Precio
		};
	}

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

	public ClienteRequest Cliente { get; set; }

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
	public int Id { get; set; }
	[Required(AllowEmptyStrings = false, ErrorMessage = "Debes introducir un nombre")]
	public string Nombre { get; set; } = null!;
	[Required(AllowEmptyStrings = false, ErrorMessage = "Debes introducir un Telefono")]

	public string Telefono { get; set; } = null!;
	public string? CorreoElectronico { get; set; }
	public string? Direcion { get; set; }

	public Cliente ToCliente()
	{
		return new()
		{
			Nombre = this.Nombre,
			Telefono = this.Telefono,
			CorreoElectronico = this.CorreoElectronico,
			Direcion = this.Direcion
		};

	}
}

//public class ReservaRequest
//{
//	public int Id { get; set; }
//	public DateTime Inicio { get; set; }
//	public DateTime Fin { get; set; }
//	public int ClienteId { get; set; }
//	public int VehiculoId { get; set; }
//	public int ServicioId { get; set; }
//	public int EmpleadoId { get; set; }
//	public string Estado { get; set; } = null!; // pendiente, completada, cancelada
//	public string? NotasAdicionales { get; set; }

//	public ClienteRequest Cliente { get; set; } = null!;
//	public VehiculoRequest Vehiculo { get; set; } = null!;
//	public ServicioRequest Servicio { get; set; } = null!;
//	public EmpleadoRequest Empleado { get; set; } = null!;

//	public Reserva ToReserva()
//	{
//		return new()
//		{
//			Inicio = this.Inicio,
//			Fin = this.Fin,
//			ClienteId = this.ClienteId,
//			VehiculoId = this.VehiculoId,
//			ServicioId = this.ServicioId,
//			EmpleadoId = this.EmpleadoId,
//			Estado = this.Estado,
//			NotasAdicionales = this.NotasAdicionales
//		};
//	}
//}

public class ReservaRequest
{
	public int Id { get; set; }

	[Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
	[DataType(DataType.DateTime, ErrorMessage = "La fecha de inicio debe ser una fecha y hora válida.")]
	public DateTime Inicio { get; set; }


	[Required(ErrorMessage = "Debe seleccionar un cliente.")]
	[Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un cliente válido.")]
	public int ClienteId { get; set; }

	[Required(ErrorMessage = "Debe seleccionar un vehículo.")]
	[Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un vehículo válido.")]
	public int VehiculoId { get; set; }

	[Required(ErrorMessage = "Debe seleccionar un servicio.")]
	[Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un servicio válido.")]
	public int ServicioId { get; set; }

	[Required(ErrorMessage = "Debe seleccionar un empleado.")]
	[Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un empleado válido.")]
	public int EmpleadoId { get; set; }

	[Required(ErrorMessage = "El estado es obligatorio.")]
	[StringLength(50, ErrorMessage = "El estado no puede superar los 50 caracteres.")]
	public string Estado { get; set; } = null!; // pendiente, completada, cancelada

	[StringLength(500, ErrorMessage = "Las notas adicionales no pueden superar los 500 caracteres.")]
	public string? NotasAdicionales { get; set; }
	public DateTime Fin{ get; set; }

	public ClienteRequest Cliente { get; set; } = null!;
	public VehiculoRequest Vehiculo { get; set; } = null!;
	public ServicioRequest Servicio { get; set; } = null!;
	public EmpleadoRequest Empleado { get; set; } = null!;

	public Reserva ToReserva()
	{
		return new()
		{
			Inicio = this.Inicio,
			Fin = this.Fin,
			ClienteId = this.ClienteId,
			VehiculoId = this.VehiculoId,
			ServicioId = this.ServicioId,
			EmpleadoId = this.EmpleadoId,
			Estado = this.Estado,
			NotasAdicionales = this.NotasAdicionales
		};
	}

}