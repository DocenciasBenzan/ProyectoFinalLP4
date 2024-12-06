namespace APP2024P4.Data.Datos
{
    public class ModeloDatos
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int MarcaId { get; set; }
        public MarcaDatos Marca { get; set; } = null!;
        public ModeloRequest ToRequest()
        => new()
        {
            Id = this.Id,
            Nombre = this.Nombre,
            MarcaId = this.MarcaId,
            Marca = this.Marca.ToRequest()
        };
    }

    //Peticion de un Modelo
    public class ModeloRequest
    {
        public int Id { get; set; } = 0;
        public string Nombre { get; set; } = null!;
		public int MarcaId { get; set; }
		public MarcaRequest Marca { get; set; } = null!;

	}
}
