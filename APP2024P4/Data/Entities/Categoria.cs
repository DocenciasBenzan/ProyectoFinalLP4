using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using APP2024P4.Data.Datos;

namespace APP2024P4.Data.Entities
{
	[Table("Categorias")]
	public class Categoria
	{
		[Key]
		public int Id { get; set; }
		public string NombreC { get; set; } = null!;

		public CategoriaDatos ToDatos()
		{
			return new CategoriaDatos()
			{
				Id = this.Id,
				Nombre = this.NombreC
			};
		}
		public static Categoria Create(string nombreC)
			=> new()
			{
				NombreC = nombreC
			};
		public bool Update(string nombreC)
		{
			var save = false;
			if (NombreC != nombreC)
			{
				NombreC = nombreC; save = true;
			}
			return save;
		}
	}
}
