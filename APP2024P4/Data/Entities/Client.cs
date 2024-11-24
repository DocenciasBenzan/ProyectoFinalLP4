using System.ComponentModel.DataAnnotations.Schema;

namespace APP2024P4.Data.Entities;

[Table("Clients")]
public class Client
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

}

