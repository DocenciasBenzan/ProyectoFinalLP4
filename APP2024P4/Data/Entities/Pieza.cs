using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using APP2024P4.Data.Response;
using APP2024P4.Data.Request;

namespace APP2024P4.Data.Entities;
public class Pieza
{
	[Key]
	public int Id { get; set; }

	[Required]
	[MaxLength(100)]
	public string Nombre { get; set; } = null!;

	[Required]
	[Column(TypeName = "decimal(10, 2)")]
	public decimal Precio { get; set; }

	public string Imagen { get; set; } = null!;
	public string Marca { get; set; } = null!;

	[Range(0, int.MaxValue, ErrorMessage = "Cantidad debe ser al menos 0")]
	public int CantidadDisponible { get; set; }
	public List<FacturaParte> FacturaPartes { get; set; } = new();
	public PiezaResponse ToResponse()
	{
		return new PiezaResponse
		{
			Id = Id,
			Nombre = Nombre,
			Precio = Precio,
			Imagen = Imagen,
			Marca = Marca,
			CantidadDisponible = CantidadDisponible
		};
	}

	public bool Actualizar(PiezaRequest request)
	{
		var cambios = false;
		if (Nombre != request.Nombre)
		{
			this.Nombre = request.Nombre;
			cambios = true;
		}
		if (Precio != request.Precio)
		{
			this.Precio = request.Precio;
			cambios = true;
		}
		if (Imagen != request.Imagen)
		{
			this.Imagen = request.Imagen;
			cambios = true;
		}
		if (Marca != request.Marca)
		{
			this.Marca = request.Marca;
			cambios = true;
		}
		if (CantidadDisponible != request.CantidadDisponible)
		{
			this.CantidadDisponible = request.CantidadDisponible;
			cambios = true;
		}
		return cambios;
	}
}
