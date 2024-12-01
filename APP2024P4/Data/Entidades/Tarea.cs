using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace APP2024P4.Data.Entidades;

[Table("Tareas")]
public class Tarea
{
    [Key]
    public int Id { get; set; }
    public string Titulo { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public string Estado { get; set; } = null!;
    public string Prioridad { get; set; } = null!;
    public string? Descripcion { get; set; }
    public int? ColaboradorId { get; set; }
    public DateTime? FechaCreacion { get; set; }
    public DateTime? FechaLimite { get; set; }
    public bool IsCompleted { get; set; }

    public static Tarea Create
        (
        string titulo,
        DateTime? fechaCreacion,
        DateTime? fechaLimite,
        bool isCompleted,
        string estado,
        string prioridad,
        string? descripcion = null,
        int? colaboradorId = null,
        string userId = null!
        )
            => new()
            {
                Titulo = titulo,
                UserId = userId,
                Estado = estado,
                FechaCreacion = fechaCreacion,
                FechaLimite = fechaLimite,
                IsCompleted = isCompleted,
                Prioridad = prioridad,
                Descripcion = descripcion,
                ColaboradorId = colaboradorId
                
            };

    public bool Update(
        string titulo,
        DateTime? fechaCreacion,
        DateTime? fechaLimite,
        bool isCompleted,
        string estado,
        string prioridad,
        string? descripcion = null,
        int? colaboradorId = null,
        string userId = null!
        )
    {
        var save = false;
        if (Titulo != titulo)
        {
            Titulo = titulo; save = true;
        }  
        if (UserId != userId)
        {
            UserId = userId; save = true;
        } 
        if (Estado != estado)
        {
            Estado = estado; save = true;
        } 
        if (Prioridad != prioridad)
        {
            Prioridad = prioridad; save = true;
        } 
        if (Descripcion != descripcion)
        {
            Descripcion = descripcion; save = true;
        }  
        if (ColaboradorId != colaboradorId)
        {
            ColaboradorId = colaboradorId; save = true;
        }
        if (FechaCreacion != fechaCreacion)
        {
            FechaCreacion = fechaCreacion; save = true;
        }
        if (FechaLimite != fechaLimite)
        {
            FechaLimite = fechaLimite; save = true;
        }
        if (IsCompleted != isCompleted)
        {
            IsCompleted = isCompleted; save = true;
        }
        return save;
    }
    [ForeignKey(nameof(UserId))]
    public virtual ApplicationUser? User { get; set; }
    [ForeignKey(nameof(ColaboradorId))]
    public virtual Colaborador? Colaboradores { get; set; }

}
