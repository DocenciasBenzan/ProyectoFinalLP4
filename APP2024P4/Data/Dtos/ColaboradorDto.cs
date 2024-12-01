namespace APP2024P4.Data.Dtos
{
    public record ColaboradorDto(
        int Id,string UserId,
        string CreadorEmail,
        string ColaboradorEmail,
        bool IsApproved)
    {
        public ColaboradorRequest ToRequest()
        => new()
        {
            UserId = UserId,
            CreadorEmail = CreadorEmail,
            ColaboradorEmail = ColaboradorEmail,
            IsApproved = IsApproved
        };
    };
}
