using System.ComponentModel.DataAnnotations;

namespace APP2024P4.Contants
{
    public static class ConsVehicle
    {
        public const int YearMin = 1885;

        public const int DoorMin = 1;

        public const decimal Positive = 0.1m;
    }
    public static class ConsEngine
    {
        public const int CylinderMin = 1;
        public const int CylinderMax = int.MaxValue;


        public const int CCMin = 1;
        public const int CCMax = int.MaxValue;


        public const double HPMin = 0.1d;
        public const double HPMax = double.MaxValue;

        public const int TopSpeedMin = int.MaxValue;


        public const double Positive = 0.1d;


        public const string Gasoline = "Gasoline";
        public const string Diesel = "Diesel";
        public const string Ethanol = "Ethanol";
        public const string Electric = "Electric";
        public const string Hybrid = "Hybrid";
        public const string Hydrogen = "Hydrogen";
    }

    public static class ConsCondition
    {
        public const string New = "Nuevo";
        public const string SemiNew = "Semi Nuevo";
        public const string Used = "Usado";
    }

    public static class ConsTypeVehicle
    {

        public const string SEDAN = "Sedan";
        public const string COUPE = "Coupe";
        public const string HATCHBACK = "Hatchback";
        public const string MINIVAN = "Minivan";
        public const string WAGON = "Wagon";
        public const string SPORTS_CAR = "Sports Car";
        public const string CONVERTIBLE = "Convertible";
        public const string PICKUP_TRUCK = "Pickup Truck";

        public const string TRUCK = "Truck";
        public const string TRAILER = "Trailer";
        public const string VAN = "Van";
        public const string BUS = "Bus";
        public const string MOTORCYCLE = "Motorcycle";
        public const string SCOOTER = "Scooter";
        public const string BIKE = "Bike";
    }

    #region Validation Attributes

    public class MaxYearCarAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is int year)
            {
                return year <= DateTime.Now.Year + 1;
            }
            return false;
        }
        public override string FormatErrorMessage(string name)
        {
            return $"{name} must be less than or equal to the {DateTime.Now.Year + 1} ";
        }
    }

    public class AtLeastOneItemListString : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is not List<string> list || list.Count == 0)
            {
                return false;
            }
            return true;
        }
    }
    #endregion

}
