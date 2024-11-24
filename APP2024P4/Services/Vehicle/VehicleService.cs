using APP2024P4.Shared;
using Microsoft.EntityFrameworkCore;
using APP2024P4.Data;
using APP2024P4.Data.Request.Vehicle;
using APP2024P4.Data.Response.Vehicle;

namespace APP2024P4.Services.Vehicle;
public interface IVehicleService
{
    Task<Result> Create(VehicleRequest request);
    Task<ResultList<VehicleResponse>> Get(string filter = "", bool All = false);

}
public class VehicleService(IApplicationDbContext context) : IVehicleService
{

    public async Task<ResultList<VehicleResponse>> Get(string filter = "", bool All = false)
    {
        try
        {
            var vehicles = await context.Vehicles.Select(x => new VehicleResponse()
            {
                Id = x.Id,
                Year = x.Year,
                Doors = x.Doors,
                Condition = x.Condition,
                Type = x.Type,
                MaxWeigh = x.MaxWeigh,
                Price = x.Price,
                Description = x.Description,
                Images = x.Images,
                ModelId = x.ModelId,
                model = new ModelResponse()
                {
                    Id = x.Model.Id,
                    Name = x.Model.Name,
                    Image = "NO",
                    BrandId = x.Model.BrandId,
                    Brand = new BrandResponse()
                    {
                        Name = x.Model.Brand.Name,
                        Image = x.Model.Brand.Image
                    }
                },
                Engine = new EngineResponse(x.Engine),
                EngineId = x.EngineId,
                Available = x.Available
            }).AsNoTracking().ToListAsync().ConfigureAwait(false);

            if (!All)
            {
                vehicles = vehicles.Where(x => x.Available).ToList();
            }

            return ResultList<VehicleResponse>.Success(vehicles);

        }
        catch (Exception Ex)
        {
            return ResultList<VehicleResponse>.Failure($"Error : {Ex}");
        }
    }


    public async Task<Result> Create(VehicleRequest request)
    {
        try
        {
            var vehicle = Data.Entities.Vehicle.Create(request);
            context.Vehicles.Add(vehicle);
            await context.SaveChangesAsync().ConfigureAwait(true);

            return Result.Success("Vehicle was added");
        }
        catch (Exception e)
        {
            return Result.Failure($"Error: {e.Message}");
        }
    }
}


