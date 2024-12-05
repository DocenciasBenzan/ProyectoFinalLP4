namespace APP2024P4.Data.Response;

public class FacturaParteResponse
{
	public int Id { get; set; }
	public int FacturaID { get; set; }
	public FacturaResponse factura { get; set; }
	public int PiezaId { get; set; }
	public PiezaResponse pieza { get; set; }
	public int Cantidad { get; set; }
}

