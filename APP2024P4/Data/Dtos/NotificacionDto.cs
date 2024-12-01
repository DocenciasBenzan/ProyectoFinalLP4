namespace APP2024P4.Data.Dtos
{
    public record NotificacionDto(int Id,string UserId, string SenderEmail, string RenderEmail, string Messege, int TareaId, bool Isread, DateTime CreatedAt)
    {
        public NotifiacioRequest ToRequest()
        => new()
        {
            Id = Id,
            UserId = UserId,
            SenderEmail = SenderEmail,
            RenderEmail = RenderEmail,
            Message = Messege,
            TareaId = TareaId,
            Isread = Isread,
            CreatedAt = CreatedAt,
        };
    };
    public class NotifiacioRequest
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public string SenderEmail { get; set; } = null!;
        public string RenderEmail { get; set; } = null!;
        public int TareaId { get; set; }
        public string Message { get; set; } = null!;
        public bool Isread { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
