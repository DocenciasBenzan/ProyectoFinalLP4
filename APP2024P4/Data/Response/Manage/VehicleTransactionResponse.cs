﻿using APP2024P4.Data.Request.Vehicle;

namespace APP2024P4.Data.Response.Manage;


public class VehicleTransactionResponse
{
	public int Id { get; set; }

	public int VehicleId { get; set; }

	public bool IsPaid { get; set; }

	public int ClientId { get; set; }

	public VehicleRequest Vehicle { get; set; } = null!;

	public ClientResponse Client { get; set; } = null!;


}
