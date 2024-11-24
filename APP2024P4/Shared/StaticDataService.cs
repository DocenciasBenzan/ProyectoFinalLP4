using APP2024P4.Contants;

namespace APP2024P4.Shared;

public interface IStaticDataService
{
    List<string> GetConditionsVehicle();
    List<string> GetVehicleTypes();
}

public class StaticDataService : IStaticDataService
{
    public List<string> GetConditionsVehicle()
    {
        return new List<string>(){
            ConsCondition.New,
            ConsCondition.SemiNew,
            ConsCondition.Used,
        };
    }
    public List<string> GetVehicleTypes()
    {
        return new List<string>()
            {
                ConsTypeVehicle.SEDAN,
                ConsTypeVehicle.COUPE,
                ConsTypeVehicle.HATCHBACK,
                ConsTypeVehicle.MINIVAN,
                ConsTypeVehicle.WAGON,
                ConsTypeVehicle.SPORTS_CAR,
                ConsTypeVehicle.CONVERTIBLE,
                ConsTypeVehicle.PICKUP_TRUCK,
                ConsTypeVehicle.TRUCK,
                ConsTypeVehicle.TRAILER,
                ConsTypeVehicle.VAN,
                ConsTypeVehicle.BUS,
                ConsTypeVehicle.MOTORCYCLE,
                ConsTypeVehicle.SCOOTER,
                ConsTypeVehicle.BIKE
            };
    }
}

