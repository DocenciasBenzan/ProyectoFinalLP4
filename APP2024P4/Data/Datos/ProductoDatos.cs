namespace APP2024P4.Data.Datos
{
    public record ProductoDato(int Id, string Nombre, int CategoriaId, DateTime FechaL, string? Color, int Cantidad, int MarcaId , int ModeloId, decimal Precio, string? Descripcion, string Imagen)
    {
        public ProductoRequest ToRequest()
        => new()
        {
            Id = this.Id,
            Nombre = this.Nombre,
            CategoriaId = this.CategoriaId,
            FechaL = this.FechaL,
            Color = this.Color,
            Cantidad = this.Cantidad,
            MarcaId = this.MarcaId,
            ModeloId = this.ModeloId,
            Precio = this.Precio,
            Descripcion = this.Descripcion,
            Imagen = this.Imagen



        };
    };
    public class ProductoRequest
    {
        public int Id { get; set; } = 0;
        public string Nombre { get; set; } = "";
        public int? CategoriaId { get; set; }
        public DateTime FechaL { get; set; }
        public string? Color { get; set; }
        public int Cantidad { get; set; }
        public int? MarcaId { get; set; }
        public int? ModeloId { get; set; }
        public decimal Precio { get; set; }
        public string? Descripcion { get; set; } = null;
        public string Imagen { get; set; } = null!;
    }
}
