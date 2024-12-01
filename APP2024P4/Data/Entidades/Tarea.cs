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
    public string? Descripcion { get; set; }
    public int? ColaboradorId { get; set; }
    public DateTime? FechaCreacion { get; set; }
    public DateTime? FechaLimite { get; set; }

    public static Tarea Create
        (
        string titulo,
        string userId,
        string? descripcion = null,
        int? colaboradorId = null
        )
            => new()
            {
                Titulo = titulo,
                UserId = userId,
                Descripcion = descripcion,
                ColaboradorId = colaboradorId
                
            };

    public bool Update(
        string titulo,
        string userId,
        string? descripcion = null,
        int? colaboradorId = null
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
        if (Descripcion != descripcion)
        {
            Descripcion = descripcion; save = true;
        }  
        if (ColaboradorId != colaboradorId)
        {
            ColaboradorId = colaboradorId; save = true;
        }
        return save;

    }
    [ForeignKey(nameof(UserId))]
    public virtual ApplicationUser? User { get; set; }
    [ForeignKey(nameof(ColaboradorId))]
    public virtual Colaborador? Colaboradores { get; set; }

}
