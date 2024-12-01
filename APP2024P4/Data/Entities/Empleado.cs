using System.ComponentModel.DataAnnotations;
using APP2024P4.Data.Response;
using APP2024P4.Data.Request;

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

	public bool Actualizar(EmpleadoRequest r)
	{
		var cambios = false;
		if (this.Nombre != r.Nombre)
		{
			Nombre = r.Nombre;
			cambios = true;
		}
		if (this.Apellido != r.Apellido) { Apellido = r.Apellido; cambios = true; }
		if (this.Telefono != r.Telefono) { Telefono = r.Telefono; cambios = true; }
		if (this.CorreoElectronico != r.CorreoElectronico) { CorreoElectronico = r.CorreoElectronico; cambios = true; }
		if (this.InicioTrabajo != r.InicioTrabajo) { InicioTrabajo = r.InicioTrabajo; cambios = true; }
		if (this.FinTrabajo != r.FinTrabajo) { FinTrabajo = r.FinTrabajo; cambios = true; }
		if (this.Activo != r.Activo)
		{
			Activo = r.Activo; 
			cambios = true;
		}
		return cambios;

	}
	public EmpleadoResponse ToResponse()
	{
		return new EmpleadoResponse()
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
