using APP2024P4.Data;
using APP2024P4.Data.Entities;
using APP2024P4.Data.Request.Manage;
using APP2024P4.Data.Response.Manage;
using APP2024P4.Data.Response.Vehicle;
using APP2024P4.Shared;
using Microsoft.EntityFrameworkCore;


namespace APP2024P4.Services.Manager;

public interface IPaymentService
{
	Task<ResultList<PaymentResponse>> GetPaymentsOfVehicle(int IdVehicleTransaction);
	Task<Result> MakePayment(PaymentRequest paymentRequest);
}

public class PaymentService(IApplicationDbContext context) : IPaymentService
{
	public async Task<Result> MakePayment(PaymentRequest paymentRequest)
	{
		try
		{
			var p = Payment.Crear(paymentRequest);
			context.Payments.Add(p);
			await context.SaveChangesAsync();
			return await CheckPaymentsOfVehicle(paymentRequest.VehicleTransaction);
		}
		catch (Exception e)
		{
			return Result.Failure($"Something was wrong:: {e.Message}");
		}
	}
	public async Task<ResultList<PaymentResponse>> GetPaymentsOfVehicle(int IdVehicleTransaction)
	{
		try
		{
			var payments = await context.Payments.Where(x => x.VehicleTransactionID == IdVehicleTransaction)
			.Select(x => new PaymentResponse()
			{
				Id = x.Id,
				Date = x.Date,
				Method = x.Method,
				Amount = x.Amount,
				VehicleTransactionID = x.VehicleTransactionID,
				VehicleTransaction = new VehicleTransactionResponse()
				{
					Id = x.VehicleTransaction.Id,
					IsPaid = x.VehicleTransaction.IsPaid,
					VehicleId = x.VehicleTransaction.VehicleId,
					ClientId = x.VehicleTransaction.ClientId,
					Vehicle = new VehicleResponse()
					{
						Id = x.VehicleTransaction.Vehicle.Id,
						Year = x.VehicleTransaction.Vehicle.Year,
						Doors = x.VehicleTransaction.Vehicle.Doors,
						Condition = x.VehicleTransaction.Vehicle.Condition,
						Type = x.VehicleTransaction.Vehicle.Type,
						MaxWeigh = x.VehicleTransaction.Vehicle.MaxWeigh,
						Price = x.VehicleTransaction.Vehicle.Price,
						Description = x.VehicleTransaction.Vehicle.Description,
						Images = x.VehicleTransaction.Vehicle.Images,
						ModelId = x.VehicleTransaction.Vehicle.ModelId,
						model = new ModelResponse()
						{
							Id = x.VehicleTransaction.Vehicle.Model.Id,
							Name = x.VehicleTransaction.Vehicle.Model.Name,
							Image = x.VehicleTransaction.Vehicle.Model.Image,
							BrandId = x.VehicleTransaction.Vehicle.Model.BrandId,
							Brand = new BrandResponse()
							{
								Name = x.VehicleTransaction.Vehicle.Model.Brand.Name,
								Image = x.VehicleTransaction.Vehicle.Model.Brand.Image
							}
						},
						Engine = new EngineResponse(x.VehicleTransaction.Vehicle.Engine),
						EngineId = x.VehicleTransaction.Vehicle.EngineId,
						Available = x.VehicleTransaction.Vehicle.Available
					}
				}
			}).ToListAsync();

			return ResultList<PaymentResponse>.Success(payments);
		}
		catch (Exception e)
		{
			return ResultList<PaymentResponse>.Failure($"Something was wrong:: {e.Message}");
		}
	}
	private async Task<Result> CheckPaymentsOfVehicle(VehicleTransactionRequest VTRequest)
	{
		try
		{
			var payments = context.Payments.Where(x => x.VehicleTransactionID == VTRequest.Id).Sum(x => x.Amount);
			if (payments >= VTRequest.Vehicle.Price)
			{
				// actualiza el estado de la transaction a pagado
				context.VehicleTransactions.First(x => x.Id == VTRequest.Id).IsPaid = true;
				await context.SaveChangesAsync().ConfigureAwait(true);
				return Result.Success("Vehiculo Pagado ✅");
			}
			else
			{
				return Result.Success($"Aun falta dinero por pagar :) {VTRequest.Vehicle.Price - payments}");

			}
		}
		catch (Exception e)
		{
			return Result.Failure($"Something was wrong:: {e.Message}");
		}
	}
}
