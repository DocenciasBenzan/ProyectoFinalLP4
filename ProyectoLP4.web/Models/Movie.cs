using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoLP4.web.Models
{
    [Table("Titulos")]
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        //public int TMDbId { get; set; } //Id de la pelicula en la API.
        public string Poster_path { get; set; }
        public string Release_date { get; set; }
        public int UserListId { get; set; }
        [ForeignKey(nameof(UserListId))]
        public virtual UserList UserList { get; set; } = new UserList();
    }
}
