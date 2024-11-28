namespace APP2024P4.Data.Dtos;

public record ProductoDto(int Id, string Nombre, byte[]? Img, int? CategoriaId, string Categoria, decimal Precio)
{
    public string PrecioText => $"RD$ {Precio.ToString("N2")}";
    public ProductoRequest ToRequest()
    => new()
    {
        Id = this.Id,
        Nombre = this.Nombre,
        Img = this.Img,
        CategoriaId = this.CategoriaId,
        Precio = this.Precio
    };
};
public class ProductoRequest
{
    public int Id { get; set; } = 0;
    public string Nombre { get; set; } = "";
    public byte[]? Img { get; set; }
    public int? CategoriaId { get; set; }
    public decimal Precio { get; set; }
}