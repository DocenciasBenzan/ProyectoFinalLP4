using APP2024P4.Components.Pages.P_home;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APP2024P4.Data.Entities;

[Table("Productos")]
public class Producto
{
    [Key]
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public byte[]? Img { get; set; }
    public int? CategoriaId { get; set; }
    [Column(TypeName = "decimal(18,6)")]
    public decimal Precio { get; set; } = 0;

    // Propiedad adicional para facilitar la visualización de la imagen como una URL base64
    [NotMapped]
    public string ImagenUrl
    {
        get
        {
            if (Img != null && Img.Length > 0)
            {
                return $"data:image/png;base64,{Convert.ToBase64String(Img)}";
            }
            return string.Empty;
        }
    }

    #region Metodos
    public static Producto Create(string nombre, byte[]? img = null, int? categoriaId = null, decimal precio = 0)
        => new()
        {
            Nombre = nombre,
            Img = img,
            CategoriaId = categoriaId,
            Precio = precio
        };
    public bool Update(string nombre, byte[] img = null, int? categoriaId = null, decimal precio = 0)
    {
        var save = false;
        if (Nombre != nombre)
        {
            Nombre = nombre; save = true;
        }
        if (Img != img)
        {
            Img = img; save = true;
        }
        if (CategoriaId != categoriaId)
        {
            CategoriaId = categoriaId; save = true;
        }
        if (Precio != precio)
        {
            Precio = precio; save = true;
        }
        return save;
    }
    #endregion Metodos
    #region Relaciones
    [ForeignKey(nameof(CategoriaId))]
    public virtual Categoria? Categoria { get; set; }

    #endregion
}
