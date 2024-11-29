using APP2024P4.Data;
using APP2024P4.Data.Response;

namespace APP2024P4.Servicios;

public interface IVehiculoServicio
{
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
}


