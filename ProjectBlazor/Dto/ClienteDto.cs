using ProjectBlazor.Entities;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;

namespace ProjectBlazor.Dto;

public record class ClienteDto(int ClienteId, string Nombre, string? Apellido, string? NumeroTelefonico, string? Cedula, string Direccion)
{
    //public int Id { get; set; }
    //public string Nombre { get; set; }
    //public string Apellido { get; set; }
    //public string NumeroTelefonico { get; set; }
    //public string Cedula { get; set; }
    //public string Direcion { get; set; }

    public ClienteRequest ToRequest()
       => new()
       {
           ClienteId = ClienteId,
           Nombre = Nombre,
           Apellido = Apellido,
           NumeroTelefonico = NumeroTelefonico,
           Cedula = Cedula,
           Direccion = Direccion
       };
}
public class ClienteRequest
{
    public int ClienteId { get; set; } = 0;
    public string Nombre { get; set; } = "";
    public string? Apellido { get; set; } = null;
    public string? NumeroTelefonico { get; set; } = null;
    public string? Cedula { get; set; } = null; 
    public string Direccion { get; set; } = ""; 
}