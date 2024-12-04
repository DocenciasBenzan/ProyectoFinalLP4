using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APP2024P4.Data.Entidades;

[Table("Comentarios")]
public class Comentario
{
    [Key]
    public int Id { get; set; }
    public string Contenido { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public string CreadorEmail { get; set; } = null!;
    [ForeignKey(nameof(Tarea.Id))]
    public int TareaId { get; set; }
    public DateTime FechaCreacion { get; set; }
    public DateTime FechaActualizacion { get; set; }

    public static Comentario Create(
       string contenido,
       string userId,
       string creadorEmail,
       int tareaId,
       DateTime fechaCreacion,
       DateTime fechaActualizacion
       )
          => new()
          {
              Contenido = contenido,
              UserId = userId,
              CreadorEmail = creadorEmail,
              TareaId = tareaId,
              FechaCreacion = fechaCreacion,
              FechaActualizacion = fechaActualizacion
          };
    public bool Update(
       string contenido,
       string userId,
       string creadorEmail,
       int tareaId,
       DateTime fechaCreacion,
       DateTime fechaActualizacion
        )
    {
        var save = false;
        if (Contenido != contenido)
        {
            Contenido = contenido;
            save = true;
        }
        if (this.UserId != userId)
        {
            this.UserId = userId;
            save = true;
        }
        if (this.CreadorEmail != creadorEmail)
        {
            this.CreadorEmail = creadorEmail;
            save = true;
        }
        if (this.TareaId != tareaId)
        {
            this.TareaId = tareaId;
            save = true;
        }
        if (this.FechaCreacion != fechaCreacion)
        {
            this.FechaCreacion = fechaCreacion;
            save = true;
        }
        if (this.FechaActualizacion != fechaActualizacion)
        {
            this.FechaActualizacion = fechaActualizacion;
            save = true;
        }
        return save;
    }

    [ForeignKey(nameof(UserId))]
    public virtual ApplicationUser? User { get; set; }
    public virtual Tarea? Tareas { get; set; }


}
