namespace APP2024P4.Data.Datos
{
    public class MarcaDatos
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public MarcaRequest ToRequest()
        => new()
        {
            Id = this.Id,
            Nombre = this.Nombre
        };
    }

    public class MarcaRequest
    {
        public int Id { get; set; } 
        public string Nombre { get; set; } = null!;
	}
}
