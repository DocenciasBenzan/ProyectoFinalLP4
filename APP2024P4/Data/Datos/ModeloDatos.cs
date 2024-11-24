namespace APP2024P4.Data.Datos
{
    public record ModeloDatos(int Id, string Nombre, int MarcaId)
    {
        public ModeloRequest ToRequest()
        => new()
        {
            Id = this.Id,
            Nombre = this.Nombre,
            MarcaId = this.MarcaId
        };
    }

    public class ModeloRequest
    {
        public int Id { get; set; } = 0;
        public string Nombre { get; set; } = "";
        public int? MarcaId { get; set; }
    }
}
