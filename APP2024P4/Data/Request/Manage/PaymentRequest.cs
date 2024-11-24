namespace APP2024P4.Data.Request.Manage;

public class PaymentRequest{
	public int Id { get; set; }

	public DateTime Date { get; set; }
	public decimal Amount { get; set; }
	public int VehicleTransactionID { get; set; }
	public string Method { get; set; } = null!;
    public VehicleTransactionRequest VehicleTransaction { get; set; }
}
