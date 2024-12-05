namespace APP2024P4.Data.Dtos
{
    /// <summary>
    /// Representa los detalles de un producto, utilizado para mostrar información en la interfaz de usuario.
    /// </summary>
    public record ProductoDto(int Id, string Nombre, byte[]? Img, int? CategoriaId, string Categoria, decimal Precio)
    {
        /// <summary>
        /// Obtiene la imagen en formato base64 para mostrarla en la interfaz.
        /// </summary>
        public string ImagenUrl => Img != null && Img.Length > 0 ? $"data:image/png;base64,{Convert.ToBase64String(Img)}" : string.Empty;

        /// <summary>
        /// Formatea el precio del producto en formato de moneda (RD$).
        /// </summary>
        public string PrecioText => $"RD$ {Precio.ToString("N2")}";

        /// <summary>
        /// Convierte el objeto ProductoDto en un objeto ProductoRequest.
        /// </summary>
        /// <returns>Un objeto ProductoRequest con los mismos valores de Id, Nombre, Img, CategoriaId, y Precio.</returns>
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

    /// <summary>
    /// Representa una solicitud para crear o actualizar un producto.
    /// </summary>
    public class ProductoRequest
    {
        /// <summary>
        /// Identificador único del producto.
        /// </summary>
        public int Id { get; set; } = 0;

        /// <summary>
        /// Nombre del producto.
        /// </summary>
        public string Nombre { get; set; } = "";

        /// <summary>
        /// Imagen del producto en formato binario (byte array).
        /// </summary>
        public byte[]? Img { get; set; }

        /// <summary>
        /// Identificador de la categoría a la que pertenece el producto.
        /// </summary>
        public int? CategoriaId { get; set; }

        /// <summary>
        /// Precio del producto.
        /// </summary>
        public decimal Precio { get; set; }
    }
}
