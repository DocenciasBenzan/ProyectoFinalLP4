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
        public string TMDbId { get; set; }
        public string Poster_path { get; set; }
        public string Release_date { get; set; }
        public decimal Vote_average { get; set; }

        //Para series
        public string Name { get; set; }
        public string First_air_date { get; set; }

    }
}
