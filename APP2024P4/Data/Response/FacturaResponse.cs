namespace APP2024P4.Data.Response;

public class FacturaResponse
{
	public int FacturaID { get; set; }
	public DateTime Fecha { get; set; }
	public decimal Total { get; set; }
	public List<FacturaParteResponse> FacturaPartes { get; set; }
	public ClienteResponse Cliente { get; set; }
}

