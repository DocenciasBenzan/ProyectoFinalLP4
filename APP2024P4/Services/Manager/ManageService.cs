using APP2024P4.Data;
using APP2024P4.Data.Entities;
using APP2024P4.Data.Request.Manage;
using APP2024P4.Data.Request.Vehicle;
using APP2024P4.Shared;

namespace APP2024P4.Services.Manager;

public class ManageService(IApplicationDbContext context)
{
	private Result ComprarVehiculo(int VehicleId, int ClientId)
	{
		// Simulando que es al contado
		try
		{
			// primero crear el payment
			var VT = new VehicleTransaction()
			{
				VehicleId = VehicleId,
				ClientId = ClientId,
				IsPaid = true // Cambiar...
			};
			//context.VehicleTransactions.Add();
			return Result.Success("OK");

		}
		catch (Exception e)
		{
			return Result.Failure($"Something was wrong:: {e.Message}");
		}

	}
}

public class PaymentService(IApplicationDbContext context)
{

	/*
	public DateTime Date { get; set; }
		public decimal Amount { get; set; }
		public int VehicleTransactionID { get; set; }
		public string Method { get; set; } = null!; 

	 */
	private async Task<Result> AgregarPayment(PaymentRequest paymentRequest)
	{
		try{
			var p = Payment.Crear(paymentRequest);
			context.Payments.Add(p);
			await context.SaveChangesAsync();
			// verificar estado de vehicle transaction y actualizar el is paid:
			// Hacer consulta para ver si ya se llego al pago total
			return Result.Success();
		}
		catch(Exception e){
			return Result.Failure($"Something was wrong:: {e.Message}");
		}
	}
}
