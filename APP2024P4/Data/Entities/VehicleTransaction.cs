using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APP2024P4.Data.Entities;

[Table("VehicleTransactions")]

public class VehicleTransaction
{
    [Key]
    public int Id { get; set; }

    public int VehicleId { get; set; }

    public bool IsPaid { get; set; }

    public int ClientId { get; set; }

    #region					ForeignKeys

    [ForeignKey(nameof(VehicleId))]
    public virtual Vehicle Vehicle { get; set; } = null!;


    [ForeignKey(nameof(ClientId))]
    public virtual Client Client { get; set; } = null!;

    #endregion


}
