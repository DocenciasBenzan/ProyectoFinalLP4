using System.ComponentModel.DataAnnotations;

namespace APP2024P4.Data.Datos
{
	public class ProductoDatos
	{
		public int Id { get; set; } = 0;
		public string Nombre { get; set; } = null!;
		public int CategoriaId { get; set; }
		public CategoriaDatos Categoria { get; set; } = null!;
		public DateTime FechaL { get; set; }
		public string? Color { get; set; }
		public int Cantidad { get; set; }
		public int ModeloId { get; set; }
		public ModeloDatos Modelo { get; set; } = null!;
		public decimal Precio { get; set; }
		public string? Descripcion { get; set; } 
		public string Imagen { get; set; } = null!;
		public string PrecioText => $"RD$ {Precio.ToString("N2")}";

		//Peticion de un Producto
		public ProductoRequest ToRequest()
		{
			return new()
			{
				Id = this.Id,
				Nombre = this.Nombre,
				CategoriaId = this.CategoriaId,
				Categoria = this.Categoria.ToRequest(),
				FechaL = this.FechaL,
				Color = this.Color,
				Cantidad = this.Cantidad,
				ModeloId = this.ModeloId,
				Modelo = this.Modelo.ToRequest(),
				Precio = this.Precio,
				Descripcion = this.Descripcion,
				Imagen = this.Imagen
			};
		}
	}

	//Validadciones de Registro
	public class ProductoRequest
	{
		public int Id { get; set; } = 0;
		[Required(AllowEmptyStrings =false,ErrorMessage = "El nombre es obligatorio.")]
		public string Nombre { get; set; } = null!;
		public int CategoriaId { get; set; }
		public CategoriaRequest Categoria { get; set; } = null!;

		public DateTime FechaL { get; set; }
		[Required(AllowEmptyStrings = false, ErrorMessage = "El color es obligatorio.")]

		public string? Color { get; set; }
		[Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que cero.")]
		public int Cantidad { get; set; }
		[Required(ErrorMessage = "El modelo es obligatorio.")]
		public int ModeloId { get; set; }
		public ModeloRequest Modelo { get; set; } = null!;
		[Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que cero.")]
		public decimal Precio { get; set; }
		public string? Descripcion { get; set; } = null;
		[Required(ErrorMessage = "La imagen es obligatoria.")]
		public string Imagen { get; set; } = null!;
	}
}
