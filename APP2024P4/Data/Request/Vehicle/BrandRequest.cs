using APP2024P4.Data.Entities;

namespace APP2024P4.Data.Request.Vehicle;

public class BrandRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Image { get; set; } = null!;
}
