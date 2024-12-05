namespace APP2024P4.Data.Request;
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

