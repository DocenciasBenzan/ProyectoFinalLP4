using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using APP2024P4.Data.Datos;

namespace APP2024P4.Data.Entities
{
	[Table("Modelos")]
	public class Modelo
	{
		[Key]
		public int Id { get; set; }
		public string NombreM { get; set; } = null!;
		public int MarcaId { get; set; }

		public static Modelo Create(ModeloRequest r)
			=> new()
			{
				NombreM = r.Nombre,
				MarcaId = r.MarcaId
			};
		public ModeloDatos ToDatos()
		{
			return new ModeloDatos()
			{
				Id = this.Id,
				Nombre = this.NombreM,
				MarcaId = this.MarcaId,
				Marca = new MarcaDatos()
				{
					Id = this.Marca.Id ,
					Nombre = this.Marca?.NombreMc ?? "ND",
				}
			};
		}
		public bool Update(ModeloRequest request)
		{
			var save = false;
			if (NombreM != request.Nombre)
			{
				NombreM = request.Nombre;
				save = true;
			}
			if (MarcaId != request.MarcaId)
			{
				MarcaId = request.MarcaId;
				save = true;
			}
			return save;

		}
		[ForeignKey(nameof(MarcaId))]
		public virtual Marca? Marca { get; set; }
	}
}
