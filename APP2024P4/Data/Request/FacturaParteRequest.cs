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

