using System.ComponentModel.DataAnnotations;
using APP2024P4.Data.Request;
using APP2024P4.Data.Response;

namespace APP2024P4.Data.Entities;

public class Cliente
{
	[Key]
	public int Id { get; set; }
	public string Nombre { get; set; } = null!;
	public string Telefono { get; set; } = null!;
	public string? CorreoElectronico { get; set; }
	public string? Direcion { get; set; }

	public bool Actualizar(ClienteRequest request)
	{
		var cambios = false;
		if (this.Nombre != request.Nombre)
		{
			this.Nombre = request.Nombre;
			cambios = true;
		}	
		if (this.Telefono != request.Telefono)
		{
			this.Telefono = request.Telefono;
			cambios = true;
		}
		if (this.CorreoElectronico != request.CorreoElectronico)
		{
			this.CorreoElectronico = request.CorreoElectronico;
			cambios = true;
		}
		if (this.Direcion != request.Direcion)
		{
			this.Direcion = request.Direcion;
			cambios = true;
		}



		return cambios;
	}
	public ClienteResponse ToResponse()
	{
		return new()
		{
			Id = this.Id,
			Nombre = this.Nombre,
			Telefono = this.Telefono,
			CorreoElectronico = this.CorreoElectronico,
			Direcion = this.Direcion
		};
	}
}
