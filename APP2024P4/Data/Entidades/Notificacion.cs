using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace APP2024P4.Data.Entidades;

[Table("Notificaciones")]
public class Notificacion
{
    [Key]
    public int Id { get; set; }
    public string SenderEmail { get; set; } = null!;
    public string RenderEmail { get; set; } = null!;
    public int TareaId { get; set; }
    public string Message { get; set; } = null!;
    public bool Isread { get; set; }
    public DateTime FechaCreacion { get; set; }
    public string Status { get; set; } = "Pending"; // "Accepted", "Rejected"

    [ForeignKey(nameof(TareaId))]
    public virtual Tarea? Tareas { get; set; }
}
