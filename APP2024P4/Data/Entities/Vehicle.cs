using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using APP2024P4.Contants;
using APP2024P4.Data.Request.Vehicle;

namespace APP2024P4.Data.Entities;

public class Vehicle
{

    #region Property

    [Key]
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

    public int Price { get; set; }
    public string Description { get; set; } = "Perfect for you";
    public bool Available { get; set; }
    public List<string> Images { get; set; } = new List<string>();
    #endregion

    #region  Foreign Keys :: Model, Engine
    public int ModelId { get; set; }
    [ForeignKey(nameof(ModelId))]
    public virtual Model Model { get; set; } = null!;


    public int EngineId { get; set; }
    [ForeignKey(nameof(EngineId))]
    public virtual Engine Engine { get; set; } = null!;

    #endregion

    #region Methods


    public static Vehicle Create(VehicleRequest r)
    {
        return new Vehicle()
        {
            Year = r.Year,
            Doors = r.Doors,
            Condition = r.Condition,
            Type = r.Type,
            MaxWeigh = r.MaxWeigh,
            Price = r.Price,
            Available = r.Available,
            Description = r.Description,
            Images = r.Images,
            ModelId = r.ModelId,
            EngineId = r.EngineId
        };
    }
    #endregion


}
