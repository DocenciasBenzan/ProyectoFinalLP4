using APP2024P4.Data.Request;

namespace APP2024P4.Data.Response;

public class FacturaResponse
{
	public int FacturaID { get; set; }
	public DateTime Fecha { get; set; }
	public decimal Total { get; set; }
	public List<FacturaParteResponse> FacturaPartes { get; set; }
	public ClienteResponse Cliente { get; set; }

	public FacturaRequest ToRequest()
	{
		return new FacturaRequest()
		{
			FacturaID = this.FacturaID,
			Fecha = this.Fecha,
			Total = this.Total,
			Cliente = new ClienteRequest() { Id = this.Cliente.Id, Nombre = this.Cliente.Nombre },
			FacturaPartes = FacturaPartes.Select(p => new FacturaParteRequest()
			{
				Id = p.Id,
				Cantidad = p.Cantidad,
				PiezaId = p.PiezaId,
				Pieza = new PiezaRequest()
				{
					Id = p.PiezaId,
					Nombre = p.pieza.Nombre,
					Precio = p.pieza.Precio,
					Imagen = p.pieza.Imagen,
					Marca = p.pieza.Marca,
					CantidadDisponible = p.pieza.CantidadDisponible,
				}


			}).ToList()
		};
	}
}

