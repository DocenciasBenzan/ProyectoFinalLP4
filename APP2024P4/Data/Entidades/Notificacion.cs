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
    public int? TareaId { get; set; }
    public string Message { get; set; } = null!;
    public bool Isread { get; set; }
    public DateTime FechaCreacion { get; set; }
    public string Status { get; set; } = "Pending"; // "Accepted", "Rejected"

    public static Notificacion Create(
        string senderEmail,
        string renderEmail,
        string message,
        bool isread,
        DateTime fechaCreacion,
        int? tareaId = null
        )
           => new()
           {
               SenderEmail = senderEmail,
               RenderEmail = renderEmail,
               TareaId = tareaId,
               Message = message,
               Isread = isread,
               FechaCreacion = fechaCreacion,
           };
    public bool Update(
        string senderEmail,
        string renderEmail,
        string message,
        bool isread,
        DateTime fechaCreacion,
        int? tareaId = null
        )
    {
        var save = false;
        if (this.SenderEmail != senderEmail)
        {
            this.SenderEmail = senderEmail;
            save = true;
        }       
        if (this.RenderEmail != renderEmail)
        {
            this.RenderEmail = renderEmail;
            save = true;
        }
        if (this.Message != message)
        {
            this.Message = message;
            save = true;
        }
        if (this.Isread != isread)
        {
            this.Isread = isread;
            save = true;
        } 
        if (this.FechaCreacion != fechaCreacion)
        {
            this.FechaCreacion = fechaCreacion;
            save = true;
        } 
        if (this.TareaId != tareaId)
        {
            this.TareaId = tareaId;
            save = true;
        }
        return save;
    }

    [ForeignKey(nameof(TareaId))]
    public virtual Tarea? Tareas { get; set; }
}
