using System.ComponentModel.DataAnnotations;

namespace APP2024P4.Data.Entities
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Image { get; set; } = null!;
        public virtual ICollection<Model> Models { get; set; } = [];

    }
}
