namespace APP2024P4.Data.Response;


public class FacturaParteResponse
{
	public int Id { get; set; }
	public int FacturaID { get; set; }
	public FacturaResponse Factura { get; set; }
	public int PiezaId { get; set; }
	public PiezaResponse Pieza { get; set; }
	public int Cantidad { get; set; }
}
public class FacturaResponse
{
	public int FacturaID { get; set; }
	public DateTime Fecha { get; set; }
	public decimal Total { get; set; }
	public List<FacturaParteResponse> FacturaPartes { get; set; }
	public ClienteResponse Cliente { get; set; }
}
public class PiezaResponse
{
	public int Id { get; set; }
	public string Nombre { get; set; }
	public decimal Precio { get; set; }
	public string Imagen { get; set; }
	public string Marca { get; set; }
	public int CantidadDisponible { get; set; }
}
public class ClienteResponse
{
	public int Id { get; set; }
	public string Nombre { get; set; }
}

