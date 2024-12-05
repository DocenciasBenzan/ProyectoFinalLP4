namespace APP2024P4.Data.Dtos
{
    public class CategoriaDto(int Id = 0, string? nombre = null)
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;

        public CategoriaRequest ToRequest() => new()
        {
            Id = this.Id,
            Nombre = this.Nombre, 
        };
    }

    public class CategoriaRequest
    {
        public int Id { get; set; } = 0;
        public string Nombre { get; set; } = "";
    }
}
