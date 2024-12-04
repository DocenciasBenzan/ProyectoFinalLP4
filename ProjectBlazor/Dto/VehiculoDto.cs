namespace ProjectBlazor.Dto;

public record class VehiculoDto(int VehiculoId, string Marca, string? Modelo, string? Matricula, string? NumeroPlaca, string? Tipo, string? Year, string? Estatus, decimal Precio = 0)
{
    //public int VehiculoId { get; set; }
    //public string Marca { get; set; }
    //public string Modelo { get; set; }
    //public string Matricula { get; set; }
    //public string NumeroPlaca { get; set; }
    //public string Tipo { get; set; }
    //public string Estatus { get; set; }
    //public decimal Precio { get; set; }
    //public string PrecioText => $"RD$ {Precio.ToString("N2")}";
    public VehiculoRequest ToRequest()
		=> new()
		{
			VehiculoId = VehiculoId,
			Marca = Marca,
			Modelo = Modelo,
			Matricula = Matricula,
			NumeroPlaca	= NumeroPlaca,
			Tipo = Tipo,
			Year = Year,
			Estatus = Estatus,
			Precio = Precio


		};

}
public class VehiculoRequest
{
	public int VehiculoId { get; set; } = 0;
	public string Marca { get; set; } = "";
	public string? Modelo { get; set; } = null;
	public string? Matricula { get; set; } = null;
	public string? NumeroPlaca { get; set; } = null;
	public string? Tipo { get; set; } = null;
	public string? Year { get; set; } = null;
	public string? Estatus { get; set; } = null;
	public decimal Precio { get; set; } = 0;
}

