using APP2024P4.Components.Pages.P_home;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APP2024P4.Data.Entities
{
    // Representa la tabla "Productos" en la base de datos.
    [Table("Productos")]
    public class Producto
    {
        // Clave primaria del producto.
        [Key]
        public int Id { get; set; }

        // Nombre del producto.
        public string Nombre { get; set; } = null!;

        // Imagen del producto almacenada como un arreglo de bytes (opcional).
        public byte[]? Img { get; set; }

        // Id de la categoría a la que pertenece el producto (opcional).
        public int? CategoriaId { get; set; }

        // Precio del producto con una precisión de hasta 6 decimales.
        [Column(TypeName = "decimal(18,6)")]
        public decimal Precio { get; set; } = 0;

        // Propiedad no mapeada para obtener la imagen como una URL base64 (no almacenada en la base de datos).
        [NotMapped]
        public string ImagenUrl
        {
            get
            {
                if (Img != null && Img.Length > 0)
                {
                    // Convierte la imagen a base64 si está presente.
                    return $"data:image/png;base64,{Convert.ToBase64String(Img)}";
                }
                return string.Empty;
            }
        }

        #region Métodos

        // Método estático para crear un nuevo producto.
        public static Producto Create(string nombre, byte[]? img = null, int? categoriaId = null, decimal precio = 0)
            => new()
            {
                Nombre = nombre,
                Img = img,
                CategoriaId = categoriaId,
                Precio = precio
            };

        // Método para actualizar los valores del producto. Retorna true si hay cambios.
        public bool Update(string nombre, byte[] img = null, int? categoriaId = null, decimal precio = 0)
        {
            var save = false;
            if (Nombre != nombre)
            {
                Nombre = nombre; save = true;
            }
            if (Img != img)
            {
                Img = img; save = true;
            }
            if (CategoriaId != categoriaId)
            {
                CategoriaId = categoriaId; save = true;
            }
            if (Precio != precio)
            {
                Precio = precio; save = true;
            }
            return save;
        }

        #endregion Métodos

        #region Relaciones

        // Relación con la entidad "Categoria". Define que un producto puede tener una categoría.
        [ForeignKey(nameof(CategoriaId))]
        public virtual Categoria? Categoria { get; set; }

        #endregion
    }
}
