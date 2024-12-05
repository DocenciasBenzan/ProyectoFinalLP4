using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;

namespace ProjectBlazor.Dto;

public record class RentaDto(int rentaId = 0, DateTime? fechaRenta = null, DateTime? fechaEntrega = null, decimal totalPagado = 0, int? vehiculoId = null, int? clienteId = null, int diasRentado = 0, decimal precio = 0)
{ 
    public RentaRequest ToRequest()
        => new()
        {
            RentaId = rentaId,
            FechaRenta = fechaRenta,
            FechaEntrega = fechaEntrega,
            TotalPagado = totalPagado,
            VehiculoId = vehiculoId,
            ClienteId = clienteId,
            DiasRentado = diasRentado,
            Precio = precio
        };
   
};
public class RentaRequest
{
    public int RentaId { get; set; } = 0;
    public DateTime? FechaRenta { get; set; } 
    public DateTime? FechaEntrega { get; set; }
    public decimal TotalPagado { get; set; } = 0;
    public int? VehiculoId { get; set; } 
    public int? ClienteId { get; set; }
    public int DiasRentado { get; set; } = 0;
    public decimal Precio { get; set; } = 0;
}

