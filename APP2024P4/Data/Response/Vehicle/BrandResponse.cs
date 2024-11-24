using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;
using APP2024P4.Contants;
using APP2024P4.Data.Entities;
using APP2024P4.Data.Request.Vehicle;

namespace APP2024P4.Data.Response.Vehicle;

public class BrandResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Image { get; set; } = null!;
}
public class ModelResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Image { get; set; } = null!;
    public int BrandId { get; set; }
    public BrandResponse Brand { get; set; } = null!;
}
public class VehicleResponse
{
    public int Id { get; set; }
    //              Y E A R 
    [Range(ConsVehicle.YearMin, int.MaxValue)]
    public int Year { get; set; }

    //              D  O O R
    [Range(ConsVehicle.DoorMin, int.MaxValue)]
    public int Doors { get; set; }


    public string Condition { get; set; } = ConsCondition.New;
    public string Type { get; set; } = ConsTypeVehicle.SEDAN;

    public int MaxWeigh { get; set; }
    public bool Available { get; set; }

    public int Price { get; set; }
    public string Description { get; set; } = "Perfect for you";
    public List<string> Images { get; set; } = new();
    public int ModelId { get; set; }
    //public ModelResponse Model { get; set; } = null!;
    public ModelResponse model { get; set; } = null!;
    public int EngineId { get; set; }

    public EngineResponse Engine { get; set; } = null!;


    public VehicleRequest ToRequest()
    {
        return new()
        {
            Id = Id,
            Year = Year,
            Doors = Doors,
            Condition = Condition,
            Type = Type,
            MaxWeigh = MaxWeigh,
            Price = Price,
            Description = Description,
            Images = Images,
            ModelId = model.Id,
            EngineId = Engine.Id
        };
    }

}

public class EngineResponse
{
    public EngineResponse(Engine engine)
    {
        Id = engine.Id;
        CC = engine.CC;
        HorsePower = engine.HorsePower;
        Cylinder = engine.Cylinder;
        TopSpeed = engine.TopSpeed;
        Turbo = engine.Turbo;
        AccelerationZeroTo100 = engine.AccelerationZeroTo100;
        AccelerationZeroTo200 = engine.AccelerationZeroTo200;
        ConsumeUrban = engine.ConsumeUrban;
        ConsumeSubUrb = engine.ConsumeSubUrb;
        FuelType = engine.FuelType;
    }

    public EngineResponse()
    {

    }

    public int Id { get; set; }
    public int CC { get; set; }
    public decimal HorsePower { get; set; }
    public int Cylinder { get; set; }
    public int TopSpeed { get; set; }
    public bool Turbo { get; set; }
    public decimal? AccelerationZeroTo100 { get; set; }
    public decimal? AccelerationZeroTo200 { get; set; }

    public decimal ConsumeUrban { get; set; }
    public decimal ConsumeSubUrb { get; set; }
    public string FuelType { get; set; } = null!;


    public string Description { get; set; }
}