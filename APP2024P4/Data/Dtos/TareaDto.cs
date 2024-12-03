namespace APP2024P4.Data.Dtos;

public record TareaDto(
 int Id,
 string UserId = null!,
 string Titulo = null!,
 string Descripcion = null!,
 string Estado = null!,
 string Prioridad = null!,
 int? ColaboradorId = null,
 DateTime? FechaCreacion = null!,
 DateTime? FechaLimite = null!,
 bool IsCompleted = false
 )
{
    public TareaRequest ToRequest()
=> new()
{
    Id = this.Id,
    UserId = this.UserId,
    Titulo = this.Titulo,
    Descripcion = this.Descripcion,
    Prioridad = this.Prioridad,
    Estado = this.Estado,
    FechaCreacion = this.FechaCreacion,
    FechaLimite = this.FechaLimite,
    IsCompleted = this.IsCompleted,

};
};
public class TareaRequest
{
    public int Id { get; set; } = 0;
    public string UserId { get; set; } = null!;
    public string Titulo { get; set; } = "";
    public int? ColaboradorId { get; set; }
    public string Descripcion { get; set; } = "";
    public string Estado { get; set; } = "";
    public string Prioridad { get; set; } = "";
    public DateTime? FechaCreacion { get; set; }
    public DateTime? FechaLimite { get; set; }
    public bool IsCompleted { get; set; }
    
}
public class ColaboradorRequest
{
    public int Id { get; set; }
    public int TareaId { get; set; }
    public string UserId { get; set; } = null!;
    public string CreadorEmail { get; set; } = null!;
    public string ColaboradorEmail { get; set; } = null!;
    public bool IsApproved { get; set; }
}

