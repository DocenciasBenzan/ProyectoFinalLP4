using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;
using ProjectBlazor.Dto;

namespace ProjectBlazor.Entities;

[Table("Rentas")]
public record class Renta
{
    [Key]
    public int RentaId { get; set; } 
    public DateTime? FechaRenta { get; set; }

    public DateTime? FechaEntrega { get; set; }
    [Column(TypeName = "decimal(18,6)")]
    public decimal TotalPagado { get; set; } = 0;
    public int DiasRentado { get; set; } = 0;
    public decimal Precio { get; set; } = 0;
    public int? VehiculoId { get; set; }
    public int? ClienteId { get; set; }

    public virtual ICollection<Vehiculo>? Vehiculos { get; set; }
    public virtual ICollection<Cliente>? Clientes { get; set; }

    public RentaDto ToDto() => new()
    {
        rentaId = this.RentaId,
        fechaRenta = this.FechaRenta,
        fechaEntrega = this.FechaEntrega,
        totalPagado = this.TotalPagado,
        vehiculoId = this.VehiculoId,
        clienteId = this.ClienteId,
        diasRentado = this.DiasRentado,
        precio = this.Precio
    };
    public static Renta Create(int rentaId, DateTime? fechaRenta = null, DateTime? fechaEntrega = null, decimal totalPagado = 0, int? vehiculoId = null, int? clienteId = null, int diasRentado = 0, decimal precio = 0)
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

    [ForeignKey(nameof(ClienteId))]
    public virtual Cliente? Cliente { get; set; }
    [ForeignKey(nameof(VehiculoId))]
    public virtual Vehiculo? Vehiculo { get; set; }
}