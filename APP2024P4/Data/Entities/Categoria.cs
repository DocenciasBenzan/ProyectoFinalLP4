using APP2024P4.Data.Dtos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APP2024P4.Data.Entities
{
    [Table("Categorias")]
    public class Categoria
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Producto>? Productos { get; set; }
        public static Categoria Create(string nombre) => new() { Nombre = nombre };
        public CategoriaDto ToDto() => new()
        {
            Id = this.Id,
            Nombre = this.Nombre,
        };
        public bool Update(string nombre)
        {
            var save = false;
            if (this.Nombre != nombre)
            {
                this.Nombre = nombre;
                save = true;
            }
            return save;
        }
    }
}
