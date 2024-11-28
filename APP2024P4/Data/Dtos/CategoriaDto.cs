namespace APP2024P4.Data.Dtos;

public record CategoriaDto(int Id, string Nombre)
{
    public CategoriaRequest ToRequest() => new()
    {
        Id = this.Id,
        Nombre = this.Nombre
    };
};


public class CategoriaRequest
{
    public int Id { get; set; } = 0;
    public string Nombre { get; set; } = string.Empty;
}
