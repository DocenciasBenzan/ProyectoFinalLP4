using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using APP2024P4.Contants;

namespace APP2024P4.Data.Entities
{
    public class Engine
    {
        [Key]
        public int Id { get; set; }


        [Range(ConsEngine.CylinderMin, ConsEngine.CylinderMax)]
        public int CC { get; set; }


        [Range(ConsEngine.HPMin, ConsEngine.HPMax), Column(TypeName = "decimal(18,2)")]

        public decimal HorsePower { get; set; }


        [Range(ConsEngine.CylinderMin, ConsEngine.CylinderMax)]
        public int Cylinder { get; set; } = ConsEngine.CylinderMin;


        [Range(ConsEngine.Positive, double.MaxValue)]
        public int TopSpeed { get; set; }

        public bool Turbo { get; set; }

        [Range(ConsEngine.Positive, (double)decimal.MaxValue), Column(TypeName = "decimal(18,2)")]
        public decimal? AccelerationZeroTo100 { get; set; }

        [Range(ConsEngine.Positive, (double)decimal.MaxValue), Column(TypeName = "decimal(18,2)")]
        public decimal? AccelerationZeroTo200 { get; set; }


        [Range(ConsEngine.Positive, (double)decimal.MaxValue), Column(TypeName = "decimal(18,2)")]
        public decimal ConsumeUrban { get; set; }


        [Range(ConsEngine.Positive, (double)decimal.MaxValue), Column(TypeName = "decimal(18,2)")]
        public decimal ConsumeSubUrb { get; set; }

        public string FuelType { get; set; } = ConsEngine.Gasoline;

        public IEnumerable<Vehicle> Vehicles { get; set; } = new List<Vehicle>();


    }
}
