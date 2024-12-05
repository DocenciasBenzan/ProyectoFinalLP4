namespace APP2024P4.Data.Dtos
{
    public record ComentarioDto
    (
        int id,
       string contenido,
       string userId,
       string creadorEmail,
       int tareaId,
       DateTime? fechaCreacion,
       DateTime? fechaActualizacion
    )
    {

        public ComentarioRequest ToRequest()
        => new()
        {
            Id = id,
            Contenido = contenido,
            UserId = userId,
            CreadorEmail = creadorEmail,
            TareaId = tareaId,
            FechaCreacion = fechaCreacion,
            FechaActualizacion = fechaActualizacion
        };
    };
    public class ComentarioRequest
    {
        public int Id { get; set; }
        public string Contenido { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public string CreadorEmail { get; set; } = null!;
        public int TareaId { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
