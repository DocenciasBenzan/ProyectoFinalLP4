using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APP2024P4.Data.Entities;

[Table("Categorias")]
public class Categoria
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Nombre { get; set; } = null!;

    public static Categoria Create(string nombre) => new()
    {
        Nombre = nombre
    };
    public bool Update(string nombre)
    {
        var S = false;
        if (Nombre != nombre) Nombre = nombre; S = true;
        return S;
    }
}