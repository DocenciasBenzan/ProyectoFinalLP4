using APP2024P4.Data;
using APP2024P4.Data.Entities;
using APP2024P4.Data.Request.Manage;
using APP2024P4.Shared;


namespace APP2024P4.Services.Manager;

public interface IManageService
{
	Task<Result> StartVehicleTransaction(int VehicleId, int ClientId);
	Task<Result> DeleteVehicleTransaction(VehicleTransactionRequest request);
}
public class ManageService(IApplicationDbContext context) : IManageService
{
	public async Task<Result> StartVehicleTransaction(int VehicleId, int ClientId)
	{
		try
		{
			var VT = new VehicleTransaction()
			{
				VehicleId = VehicleId,
				ClientId = ClientId,
				IsPaid = false
			};
			context.VehicleTransactions.Add(VT);
			await context.SaveChangesAsync().ConfigureAwait(true);
			return Result.Success();

		}
		catch (Exception e)
		{
			return Result.Failure($"Something was wrong:: {e.Message}");
		}

	}

	public async Task<Result> DeleteVehicleTransaction(VehicleTransactionRequest request)
	{
		try
		{
			context.VehicleTransactions.Remove(request.ToTransaction());
			await context.SaveChangesAsync().ConfigureAwait(true);
			return Result.Success("Transaction eliminada correctamente");
		}
		catch (Exception ex)
		{
			return Result.Failure($"Something was wrong:: {ex.Message}");

		}
	}
}
