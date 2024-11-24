namespace APP2024P4.Data.Response.Manage;

public class PaymentResponse
{
	public int Id { get; set; }

	public DateTime Date { get; set; }
	public decimal Amount { get; set; }
	public int VehicleTransactionID { get; set; }
	public string Method { get; set; } = null!;
	public VehicleTransactionResponse VehicleTransaction { get; set; }
}
