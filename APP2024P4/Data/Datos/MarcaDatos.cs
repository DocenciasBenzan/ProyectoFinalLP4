namespace APP2024P4.Data.Datos
{
    public record MarcaDatos(int Id, string Nombre)
    {
        public MarcaRequest ToRequest()
        => new()
        {
            Id = this.Id,
            Nombre = this.Nombre
        };
    }

    public class MarcaRequest
    {
        public int Id { get; set; } = 0;
        public string Nombre { get; set; } = "";
    }
}
