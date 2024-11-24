using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ProyectoLP4.web.Models
{
    [Table("ListaDeUsuario")]
    public class UserList
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual List<Movie> Movies { get; set; } = new();
    }
}
