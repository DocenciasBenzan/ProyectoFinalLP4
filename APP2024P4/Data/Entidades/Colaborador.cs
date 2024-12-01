using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APP2024P4.Data.Entidades
{
    [Table("Colaboradores")]
    public class Colaborador
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public virtual ICollection<Tarea>? Tareas { get; set; }
        public string CreadorEmail { get; set; } = null!;
        public string ColaboradorEmail { get; set; } = null!;
        public bool IsApproved { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser? User { get; set; }
    }
}
