namespace APP2024P4.Data.Dtos
{
    public record ColaboradorDto(
        int Id,
        string UserId,
        int TareaId,
        string CreadorEmail,
        string ColaboradorEmail,
        bool IsApproved,
        bool IsCompleted
        )
    {
        public ColaboradorRequest ToRequest()
        => new()
        {
            Id = Id,
            TareaId = TareaId,
            UserId = UserId,
            CreadorEmail = CreadorEmail,
            ColaboradorEmail = ColaboradorEmail,
            IsApproved = IsApproved,
            IsCompleted = IsCompleted
        };
    };
}