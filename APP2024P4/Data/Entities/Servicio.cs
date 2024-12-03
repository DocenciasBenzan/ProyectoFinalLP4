using System.ComponentModel.DataAnnotations;
using APP2024P4.Data.Request;
using APP2024P4.Data.Response;

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

	#region Metodos

	public bool Actualizar(ServicioRequest request)
	{
		var cambios = false;
		if (this.Nombre != request.Nombre)
		{
			this.Nombre = request.Nombre;
			cambios = true;
		}
		if (this.Descripcion != request.Descripcion)
		{
			this.Descripcion = request.Descripcion;
			cambios = true;
		}
		if (this.DuracionEstimada != request.DuracionEstimada)
		{
			this.DuracionEstimada = request.DuracionEstimada;
			cambios = true;
		}
		if (this.Precio != request.Precio)
		{
			this.Precio = request.Precio;
			cambios = true;
		}
		return cambios;
	}
	public ServicioResponse ToResponse()
	{
		return new ServicioResponse
		{
			Id = this.Id,
			Nombre = this.Nombre,
			Descripcion = this.Descripcion,
			DuracionEstimada = this.DuracionEstimada,
			Precio = this.Precio
		};
	}

	#endregion
}
