namespace APP2024P4.Data.Dtos
{
    /// <summary>
    /// Representa una categoría con los detalles necesarios para la visualización.
    /// </summary>
    public record CategoriaDto(int Id, string Nombre)
    {
        /// <summary>
        /// Convierte el objeto CategoriaDto a un objeto CategoriaRequest.
        /// </summary>
        /// <returns>Un objeto CategoriaRequest con los mismos valores de Id y Nombre.</returns>
        public CategoriaRequest ToRequest() => new()
        {
            Id = this.Id,
            Nombre = this.Nombre
        };
    };

    /// <summary>
    /// Representa una solicitud para crear o actualizar una categoría.
    /// </summary>
    public class CategoriaRequest
    {
        /// <summary>
        /// Identificador único de la categoría.
        /// </summary>
        public int Id { get; set; } = 0;

        /// <summary>
        /// Nombre de la categoría.
        /// </summary>
        public string Nombre { get; set; } = string.Empty;
    }
}
