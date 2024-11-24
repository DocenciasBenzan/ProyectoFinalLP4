namespace APP2024P4.Data.Request.Vehicle;

public class ModelRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Image { get; set; } = null!;
    public int BrandId { get; set; }

}
