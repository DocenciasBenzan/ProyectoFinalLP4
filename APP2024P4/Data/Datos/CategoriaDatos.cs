namespace APP2024P4.Data.Datos;


public class CategoriaDatos(){
	public int Id { get; set; }
	public string Nombre { get; set; } = null!;

	public CategoriaRequest	ToRequest(){
		return new CategoriaRequest()
		{
			Id = this.Id,
			Nombre = this.Nombre
		};
	}
}

//Peticion de una acategoria
public class CategoriaRequest 
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
}