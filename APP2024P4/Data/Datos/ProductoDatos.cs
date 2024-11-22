namespace APP2024P4.Data.Datos
{
    public record ProductoDato(int Id, string Nombre, int CategoriaId, DateTime FechaL, string? Color, int Cantidad, int ModeloId, decimal Precio, string? Descripcion, string? Imagen )
    {
        public ProductoRequest ToRequest()
        => new()
        {
            Id = this.Id,
            Nombre = this.Nombre,
            CategoriaId = this.CategoriaId,
            //FechaL = this.FechaL

            
        };
    };
    public class ProductoRequest
    {
        public int Id { get; set; } = 0;
        public string Nombre { get; set; } = "";
        public string? Descripcion { get; set; } = null;
        public int? CategoriaId { get; set; }
        public decimal Precio { get; set; }
    }
}
