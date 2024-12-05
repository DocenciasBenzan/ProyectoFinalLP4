using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APP2024P4.Data.Entities
{
    /// <summary>
    /// Representa una categoría en el sistema.
    /// </summary>
    [Table("Categorias")]
    public class Categoria
    {
        /// <summary>
        /// Identificador único de la categoría.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Nombre de la categoría.
        /// </summary>
        [Required]
        public string Nombre { get; set; } = null!;

        /// <summary>
        /// Crea una nueva instancia de la categoría con el nombre proporcionado.
        /// </summary>
        /// <param name="nombre">Nombre de la nueva categoría.</param>
        /// <returns>Una nueva instancia de la clase Categoria.</returns>
        public static Categoria Create(string nombre) => new()
        {
            Nombre = nombre
        };

        /// <summary>
        /// Actualiza el nombre de la categoría.
        /// </summary>
        /// <param name="nombre">Nuevo nombre para la categoría.</param>
        /// <returns>True si el nombre fue actualizado, de lo contrario false.</returns>
        public bool Update(string nombre)
        {
            var S = false;
            if (Nombre != nombre)
            {
                Nombre = nombre;
                S = true;
            }
            return S;
        }
    }
}
