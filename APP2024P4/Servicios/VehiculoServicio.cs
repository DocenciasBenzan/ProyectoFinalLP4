using APP2024P4.Data;
using APP2024P4.Data.Request;
using APP2024P4.Data.Response;

namespace APP2024P4.Servicios;

public interface IVehiculoServicio
{
	Task<Result> ActualizarVehiculo(VehiculoRequest request);
	Task<Result> AgregarVehiculo(VehiculoRequest request);
	Task<Result> EliminarVehiculo(int VehiculoId);
	List<string> ObtenerColores();
	List<string> ObtenerMarcasAutos();
	List<string> ObtenerModelosAutos();
	List<string> ObtenerTiposAutos();
	ResultList<VehiculoResponse> ObtenerVehiculos(int ClientId);
}

public class VehiculoServicio(IApplicationDbContext context) : IVehiculoServicio
{
	public ResultList<VehiculoResponse> ObtenerVehiculos(int ClientId)
	{
		try
		{
			var r = context.Vehiculos.Where(x => x.ClientId == ClientId).Select(
			x => new VehiculoResponse()
			{
				Id = x.Id,
				Placa = x.Placa,
				Marca = x.Marca,
				Modelo = x.Modelo,
				Color = x.Color,
				Tipo = x.Tipo,
				ClienteId = x.Id,
				Cliente = x.Cliente.ToResponse()
			}).OrderBy(x => x.Modelo).ToList();
			Console.WriteLine($"Desde el servicio de vehiculos:: {r.Count} apeticion de::: {ClientId} ::: ");
			if (r is not null)
			{
				return ResultList<VehiculoResponse>.Success(r);
			}
			return ResultList<VehiculoResponse>.Failure("Sin vehiculos encontrados");
		}
		catch (Exception ex)
		{
			return ResultList<VehiculoResponse>.Failure($"Error cargando los Vehiculos: ${ex.Message}");
		}
	}
	public async Task<Result> AgregarVehiculo(VehiculoRequest request)
	{
		try
		{
			var cliente = context.Clientes.FirstOrDefault(x => x.Id == request.ClienteId);
			if (cliente == null)
			{
				return Result.Failure("Cliente no encontrado.");
			}
			var vehiculo = request.ToVehiculo();
			context.Vehiculos.Add(vehiculo);
			await context.SaveChangesAsync().ConfigureAwait(false);
			return Result.Success();
		}
		catch (Exception ex)
		{
			return Result.Failure($"Error al agregar el vehiculo : {ex.Message}");
		}
	}

	public async Task<Result> ActualizarVehiculo(VehiculoRequest request)
	{
		try
		{
			var Vehiculo = context.Vehiculos.FirstOrDefault(x => x.Id == request.Id);
			if (Vehiculo == null)
			{
				return Result.Failure("Vehiculo no encontrado:");
			}
			if (Vehiculo.Actualizar(request))
			{
				await context.SaveChangesAsync().ConfigureAwait(false);
				return Result.Success();
			}
			return Result.Success(" No has realizado ningun cambio!");
		}
		catch (Exception ex)
		{
			return Result.Failure($"Error al agregar el Vehiculo : {ex.Message}");
		}
	}
	public async Task<Result> EliminarVehiculo(int VehiculoId)
	{
		try
		{
			var Vehiculo = context.Vehiculos.FirstOrDefault(x => x.Id == VehiculoId);
			if (Vehiculo is not null)
			{
				context.Vehiculos.Remove(Vehiculo);
				await context.SaveChangesAsync();
				return Result.Success();
			}
			return Result.Failure("Vehiculo no encontrado");
		}
		catch (Exception ex)
		{
			return Result.Failure($"Error al agregar el Vehiculo : {ex}");
		}
	}

	public List<string> ObtenerColores()
	{
		return new List<string>
		{
			"Rojo",
			"Azul",
			"Verde",
			"Negro",
			"Blanco",
			"Amarillo",
			"Naranja",
			"Rosa",
			"Morado",
			"Gris",
			"Marrón",
			"Turquesa",
			"Cian",
			"Violeta",
			"Beige"
		};
	}
	public List<string> ObtenerModelosAutos()
	{
		return new List<string>
		{
			"Toyota Corolla",
			"Honda Civic",
			"Ford Focus",
			"Chevrolet Malibu",
			"BMW Serie 3",
			"Audi A4",
			"Mercedes-Benz Clase C",
			"Volkswagen Golf",
			"Hyundai Elantra",
			"Nissan Altima",
			"Kia Optima",
			"Mazda 3",
			"Subaru Impreza",
			"Tesla Model 3",
			"Volvo S60"
		};
	}
	public List<string> ObtenerTiposAutos()
	{
		return new List<string>
		{
			"Sedán",
			"SUV",
			"Hatchback",
			"Coupé",
			"Convertible",
			"Camioneta",
			"Van",
			"Wagon",
			"Pickup",
			"Minivan",
			"Deportivo",
			"Crossover"
		};
	}
	public List<string> ObtenerMarcasAutos()
	{
		return new List<string>
		{
			"Toyota",
			"Honda",
			"Ford",
			"Chevrolet",
			"BMW",
			"Audi",
			"Mercedes-Benz",
			"Volkswagen",
			"Hyundai",
			"Nissan",
			"Kia",
			"Mazda",
			"Subaru",
			"Tesla",
			"Volvo"
		};
	}
}
