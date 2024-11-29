namespace APP2024P4.Data.Request;


public class FacturaParteRequest
{
	public int Id { get; set; }
	public int FacturaID { get; set; }
	public FacturaRequest Factura { get; set; }
	public int PiezaId { get; set; }
	public PiezaRequest Pieza { get; set; }
	public int Cantidad { get; set; }
}
public class FacturaRequest
{
	public int FacturaID { get; set; }
	public DateTime Fecha { get; set; }
	public decimal Total { get; set; }
	public List<FacturaParteRequest> FacturaPartes { get; set; }
	public ClienteRequest Cliente { get; set; }
}
public class PiezaRequest
{
	public int Id { get; set; }
	public string Nombre { get; set; }
	public decimal Precio { get; set; }
	public string Imagen { get; set; }
	public string Marca { get; set; }
	public int CantidadDisponible { get; set; }
}
public class ClienteRequest
{
	public int Id { get; set; }
	public string Nombre { get; set; }
}

