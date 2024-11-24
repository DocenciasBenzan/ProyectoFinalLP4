using APP2024P4.Data.Entities;
using APP2024P4.Data.Request.Vehicle;
using System.ComponentModel.DataAnnotations.Schema;

namespace APP2024P4.Data.Request.Manage;

public class VehicleTransactionRequest
{
	public int Id { get; set; }

	public int VehicleId { get; set; }

	public bool IsPaid { get; set; }

	public int ClientId { get; set; }

	public VehicleRequest Vehicle { get; set; } = null!;

	public ClientRequest Client { get; set; } = null!;


}
