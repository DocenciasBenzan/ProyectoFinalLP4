using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using APP2024P4.Data.Request.Manage;

namespace APP2024P4.Data.Entities;

[Table("Payments")]

public class Payment
{
    [Key]
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public int VehicleTransactionID { get; set; }
    public string Method { get; set; } = null!;
   
    #region Methods
    public static Payment Crear(PaymentRequest request){
        return new()
        {
            Date = request.Date,
            Amount = request.Amount,
            VehicleTransactionID = request.VehicleTransactionID
        };
    }
	#endregion

    #region Foreign Keys
    [ForeignKey(nameof(VehicleTransactionID))]
    public virtual VehicleTransaction VehicleTransaction { get; set; } = null!;
    #endregion



}
