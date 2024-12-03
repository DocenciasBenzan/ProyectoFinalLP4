using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace APP2024P4.Data.Entidades;

[Table("Notificaciones")]
public class Notificacion
{
    [Key]
    public int Id { get; set; }
    public string UserId { get; set; } = null!;
    public string SenderEmail { get; set; } = null!;
    public string RenderEmail { get; set; } = null!;
    [ForeignKey(nameof(Tarea.Id))]
    public int TareaId { get; set; }
    public string Message { get; set; } = null!;
    public bool Isread { get; set; }
    public DateTime FechaCreacion { get; set; }
    public string Status { get; set; } = "Pending"; // "Accepted", "Rejected"

    public static Notificacion Create(
        string userId,
        string senderEmail,
        string renderEmail,
        string message,
        bool isread,
        DateTime fechaCreacion,
        int tareaId 
        )
           => new()
           {
               UserId = userId,
               SenderEmail = senderEmail,
               RenderEmail = renderEmail,
               TareaId = tareaId,
               Message = message,
               Isread = isread,
               FechaCreacion = fechaCreacion,
           };
    public bool Update(
        string userId,
        string senderEmail,
        string renderEmail,
        string message,
        bool isread,
        DateTime fechaCreacion,
        int tareaId 
        )
    {
        var save = false;

        if (this.UserId != userId)
        {
            this.UserId = userId;
            save = true;
        }   
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
    [ForeignKey(nameof(UserId))]
    public virtual ApplicationUser? User { get; set; }
    public virtual Tarea? Tareas { get; set; }

}
