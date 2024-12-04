using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using APP2024P4.Data.Datos;

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
		public int ModeloId { get; set; }
		public decimal Precio { get; set; }
		public string? Descripcion { get; set; }
		public string Imagen { get; set; } = null!;

		#region Metodos
		public static Producto Create(ProductoRequest request)
			=> new()
			{
				Nombre = request.Nombre,
				CategoriaId = request.CategoriaId,
				FechaL = request.FechaL,
				Color = request.Color,
				Cantidad = request.Cantidad,
				ModeloId = request.ModeloId,
				Precio = request.Precio,
				Descripcion = request.Descripcion,
				Imagen = request.Imagen
			};

		public bool Update(ProductoRequest request)
		{
			var save = false;
			if (this.Nombre != request.Nombre)
			{
				this.Nombre = request.Nombre;
				save = true;
			}
			if (this.CategoriaId != request.CategoriaId)
			{
				this.CategoriaId = request.CategoriaId;
				save = true;
			}

			if (this.FechaL != request.FechaL)
			{
				this.FechaL = request.FechaL;
				save = true;
			}

			if (this.Color != request.Color)
			{
				this.Color = request.Color;
				save = true;
			}

			if (this.Cantidad != request.Cantidad)
			{
				this.Cantidad = request.Cantidad;
				save = true;
			}

			if (this.ModeloId != request.ModeloId)
			{
				this.ModeloId = request.ModeloId;
				save = true;
			}

			if (this.Precio != request.Precio)
			{
				this.Precio = request.Precio;
				save = true;
			}

			if (this.Descripcion != request.Descripcion)
			{
				this.Descripcion = request.Descripcion;
				save = true;
			}

			if (this.Imagen != request.Imagen)
			{
				Console.WriteLine("--------------------------------Cambiando imagen ");
				this.Imagen = request.Imagen;
				save = true;
			}
			return save;
		}

		public ProductoDatos ToDatos()
		{
			return new ProductoDatos()
			{
				Id = this.Id,
				Nombre = this.Nombre,
				CategoriaId = this.CategoriaId,
				Categoria = new CategoriaDatos()
				{
					Id = this.Categoria.Id,
					Nombre = this.Categoria?.NombreC ?? "ND",
				},
				FechaL = this.FechaL,
				Color = this.Color,
				Cantidad = this.Cantidad,
				ModeloId = this.ModeloId,
				Modelo = new ModeloDatos()
				{
					Id = this.Modelo.Id,
					Nombre = this.Modelo?.NombreM ?? "ND",
					MarcaId = this.Modelo.MarcaId,
					Marca = new MarcaDatos()
					{
						Id = this.Modelo.Marca.Id,
						Nombre = this.Modelo?.Marca?.NombreMc ?? "ND"
					}
				},
				Precio = this.Precio,
				Descripcion = this.Descripcion,
				Imagen = this.Imagen
			};
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
