

namespace APP2024P4.Data.Request;

public class EmpleadoRequest
{
    public string Nombre { get; set; } = null!;
    public string Apellido { get; set; } = null!;
    public string Telefono { get; set; } = null!;
    public string CorreoElectronico { get; set; } = null!;
    public DateTime InicioTrabajo { get; set; }
    public DateTime FinTrabajo { get; set; }
    public bool Activo { get; set; }
}
public class ServicioRequest
{
    public string Nombre { get; set; } = null!;
    public string Descripcion { get; set; } = null!;
    public decimal DuracionEstimada { get; set; }
    public decimal Precio { get; set; }
}
public class VehiculoRequest
{
    public string Placa { get; set; } = null!;
    public string Marca { get; set; } = null!;
    public string Modelo { get; set; } = null!;
    public string Color { get; set; } = null!;
    public string Tipo { get; set; } = null!;
    public int ClienteId { get; set; }
}
public class ClienteRequest
{
    public string Nombre { get; set; } = null!;
    public string Telefono { get; set; } = null!;
    public string? CorreoElectronico { get; set; }
    public string? Direcion { get; set; }
}

public class ReservaRequest
{
    public DateTime Inicio { get; set; }
    public DateTime Fin { get; set; }
    public int ClienteId { get; set; }
    public int VehiculoId { get; set; }
    public int ServicioId { get; set; }
    public int EmpleadoId { get; set; }
    public string Estado { get; set; } = null!; // pendiente, completada, cancelada
    public string? NotasAdicionales { get; set; }
}
