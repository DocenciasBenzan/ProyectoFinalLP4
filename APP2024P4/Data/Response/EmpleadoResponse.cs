

using APP2024P4.Data.Request;
using Humanizer.DateTimeHumanizeStrategy;

namespace APP2024P4.Data.Response;



public class EmpleadoResponse
{
	public int Id { get; set; }
	public string Nombre { get; set; } = null!;
	public string Apellido { get; set; } = null!;
	public string Telefono { get; set; } = null!;
	public string CorreoElectronico { get; set; } = null!;
	public DateTime InicioTrabajo { get; set; }
	public DateTime FinTrabajo { get; set; }
	public bool Activo { get; set; }
}
public class ServicioResponse
{
	public int Id { get; set; }
	public string Nombre { get; set; } = null!;
	public string Descripcion { get; set; } = null!;
	public decimal DuracionEstimada { get; set; }
	public decimal Precio { get; set; }

	public ServicioRequest ToRequest()
	{
		return new ServicioRequest()
		{
			Id = this.Id,
			Nombre = this.Nombre,
			Descripcion = this.Descripcion,
			DuracionEstimada = this.DuracionEstimada,
			Precio = this.Precio
		};
	}
}
public class VehiculoResponse
{
	public int Id { get; set; }
	public string Placa { get; set; } = null!;
	public string Marca { get; set; } = null!;
	public string Modelo { get; set; } = null!;
	public string Color { get; set; } = null!;
	public string Tipo { get; set; } = null!;
	public int ClienteId { get; set; }
	public ClienteResponse Cliente { get; set; } = null!;
}
public class ClienteResponse
{
	public int Id { get; set; }
	public string Nombre { get; set; } = null!;
	public string Telefono { get; set; } = null!;
	public string? CorreoElectronico { get; set; }
	public string? Direcion { get; set; }

	public ClienteRequest ToRequest()
	{
		return new ClienteRequest()
		{
			Id = this.Id,
			Nombre = this.Nombre,
			Telefono = this.Telefono,
			CorreoElectronico = this.CorreoElectronico,
			Direcion = this.Direcion
		};
	}
}

public class ReservaResponse
{
	public int Id { get; set; }
	public DateTime Inicio { get; set; }
	public DateTime Fin { get; set; }
	public int ClienteId { get; set; }
	public int VehiculoId { get; set; }
	public int ServicioId { get; set; }
	public int EmpleadoId { get; set; }
	public string Estado { get; set; } = null!;
	public string? NotasAdicionales { get; set; }

	public ClienteResponse Cliente { get; set; } = null!;
	public VehiculoResponse Vehiculo { get; set; } = null!;
	public ServicioResponse Servicio { get; set; } = null!;
	public EmpleadoResponse Empleado { get; set; } = null!;
}
