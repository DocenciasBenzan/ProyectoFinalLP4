using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APP2024P4.Data.Entities;
[Table("Bills")]
public class Bill
{
    [Key]
    public int Id { get; set; }
    public int VehicleTransactionID { get; set; }

    public string Description { get; set; } = null!;


    #region Relations

    [ForeignKey(nameof(VehicleTransactionID))]
    public virtual VehicleTransaction VehicleTransaction { get; set; } = null!;
    #endregion


}
