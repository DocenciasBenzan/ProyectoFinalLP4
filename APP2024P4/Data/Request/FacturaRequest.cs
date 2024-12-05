namespace APP2024P4.Data.Request;

public class FacturaRequest
{
	public int FacturaID { get; set; }
	public DateTime Fecha { get; set; }
	public decimal Total { get; set; }
	public List<FacturaParteRequest> FacturaPartes { get; set; } = new List<FacturaParteRequest>();
	public ClienteRequest Cliente { get; set; }
}

