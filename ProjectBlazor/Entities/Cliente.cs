using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectBlazor.Entities;

[Table("Clientes")]
public class Cliente
{
    [Key]
    public int ClienteId { get; set; }

    public string Nombre { get; set; } = "";
    public string? Apellido { get; set; } = null;
    public string? NumeroTelefonico { get; set; } = null;
    public string? Cedula { get; set; } = null; // Added Cedula
    public string Direccion { get; set; } = ""; // Added Direccion

    // Create method updated to include Cedula and Direccion
    public static Cliente Create(string nombre, string? apellido, string? numeroTelefonico, string? cedula, string direccion)
    {
        return new()
        {
            Nombre = nombre,
            Apellido = apellido,
            NumeroTelefonico = numeroTelefonico,
            Cedula = cedula,
            Direccion = direccion
        };
    }

    // Update method updated to include Cedula and Direccion
    public bool Update(string nombre, string? apellido, string? numeroTelefonico, string? cedula, string direccion)
    {
        bool save = false;

        if (Nombre != nombre)
        {
            Nombre = nombre;
            save = true;
        }

        if (Apellido != apellido)
        {
            Apellido = apellido;
            save = true;
        }

        if (NumeroTelefonico != numeroTelefonico)
        {
            NumeroTelefonico = numeroTelefonico;
            save = true;
        }

        if (Cedula != cedula)
        {
            Cedula = cedula;
            save = true;
        }

        if (Direccion != direccion)
        {
            Direccion = direccion;
            save = true;
        }

        return save;
    }
}