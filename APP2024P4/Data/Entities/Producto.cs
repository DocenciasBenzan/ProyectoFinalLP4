using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APP2024P4.Data.Entities
{
    [Table("Poductos")]
    public class Producto
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int CategoriaId { get; set; }
        public DateTime FechaL { get; set; }
        public string? Color { get; set; }
        public int Cantidad { get; set; }
        public int MarcaId { get; set; }
        public int ModeloId { get; set; }
        public decimal Precio { get; set; } = 0;
        public string? Descripcion { get; set; }
        public string Imagen { get; set; } = null!;

        #region Metodos
        public static Producto Create(string nombre, int categoriaId, DateTime fechaL, string color, int cantidad, string imagen, int marcaId , int modeloId, decimal precio = 0, string? descripcion = null)
            => new()
            {
                Nombre = nombre,
                CategoriaId = categoriaId,
                FechaL = fechaL,
                Color = color,
                Cantidad = cantidad,
                MarcaId = marcaId,
                ModeloId = modeloId,
                Precio = precio,
                Descripcion = descripcion,
                Imagen = imagen
            };

        public bool Update(string nombre, int categoriaId, DateTime fechaL, string color, int cantidad, string imagen, int marcaId , int modeloId, decimal precio = 0, string? descripcion = null)
        {
            var save = false;
            if (Nombre != nombre)
            {
                Nombre = nombre; save = true;
            }
            if (CategoriaId != categoriaId)
            {
                CategoriaId = categoriaId; save = true;
            }
            if (FechaL != fechaL)
            {
                FechaL = fechaL; save = true;
            }
            if (Color != color)
            {
                Color = color; save = true;
            }
            if (Cantidad != cantidad)
            {
                Cantidad = cantidad; save = true;
            }
            if (Imagen != imagen)
            {
                Imagen = imagen; save = true;
            }
            if (MarcaId != marcaId)
            {
                MarcaId = marcaId; save = true;
            }
            if (ModeloId != modeloId)
            {
                ModeloId = modeloId; save = true;
            }
            if (Precio != precio)
            {
                Precio = precio; save = true;
            }
            if (Descripcion != descripcion)
            {
                Descripcion = descripcion; save = true;
            }
            return save;
        }
        #endregion Metodos

        #region Relaciones
        
        [ForeignKey(nameof(ModeloId))]
        public virtual Modelo? Modelo { get; set; }

        [ForeignKey(nameof(CategoriaId))]
        public virtual Categoria? Categoria { get; set; }

        #endregion
    }
}
