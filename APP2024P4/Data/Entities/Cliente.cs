using System.ComponentModel.DataAnnotations;
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
