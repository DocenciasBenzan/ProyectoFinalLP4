using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using APP2024P4.Data.Request;

namespace APP2024P4.Data.Entities;

public class Vehiculo
{
	[Key]
	public int Id { get; set; }
	public string Placa { get; set; } = null!;
	public string Marca { get; set; } = null!;
	public string Modelo { get; set; } = null!;
	public string Color { get; set; } = null!;
	public string Tipo { get; set; } = null!; // sedán, SUV, camioneta
	public int ClientId { get; set; }
	[ForeignKey(nameof(ClientId))]
	public virtual Cliente Cliente { get; set; }

	public bool Actualizar(VehiculoRequest r)
	{
		var cambios = false;
		if (this.Placa != r.Placa) { Placa = r.Placa; cambios = true; }
		if (this.Marca != r.Marca) { Marca = r.Marca; cambios = true; }
		if (this.Modelo != r.Modelo) { Modelo = r.Modelo; cambios = true; }
		if (this.Color != r.Color) { Color = r.Color; cambios = true; }
		if (this.Tipo != r.Tipo) { Tipo = r.Tipo; cambios = true; }
		if (this.ClientId != r.ClienteId) { ClientId = r.ClienteId; cambios = true; }
		return cambios;
	}
}
