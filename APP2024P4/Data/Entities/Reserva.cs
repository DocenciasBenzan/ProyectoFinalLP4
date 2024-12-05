using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using APP2024P4.Data.Request;
using APP2024P4.Data.Response;

namespace APP2024P4.Data.Entities;

public class Reserva
{
	[Key]
	public int Id { get; set; }
	public DateTime Inicio { get; set; }
	public DateTime Fin { get; set; }
	public int ClienteId { get; set; }
	public int VehiculoId { get; set; }
	public int ServicioId { get; set; }
	public int EmpleadoId { get; set; }
	public string Estado { get; set; } = null!;
	public string? NotasAdicionales { get; set; }

	#region Foreign Keys
	[ForeignKey(nameof(ClienteId))]
	public virtual Cliente Cliente { get; set; } = null!;
	[ForeignKey(nameof(ServicioId))]
	public virtual Servicio Servicio { get; set; } = null!;
	[ForeignKey(nameof(EmpleadoId))]
	public virtual Empleado Empleado { get; set; } = null!;
	[ForeignKey(nameof(VehiculoId))]
	public virtual Vehiculo Vehiculo { get; set; } = null!;

	#endregion

	#region Metodos
	public bool Actualizar(ReservaRequest request)
	{
		var cambios = false;

		if (Inicio != request.Inicio)
		{
			this.Inicio = request.Inicio;
			cambios = true;
		}
		if (Fin != request.Fin)
		{
			this.Fin = request.Fin;
			cambios = true;
		}
		if (ClienteId != request.ClienteId)
		{
			this.ClienteId = request.ClienteId;
			cambios = true;
		}
		if (ServicioId != request.ServicioId)
		{
			this.ServicioId = request.ServicioId;
			cambios = true;
		}
		if (VehiculoId != request.VehiculoId)
		{
			this.VehiculoId = request.VehiculoId;
			cambios = true;
		}
		if (EmpleadoId != request.EmpleadoId)
		{
			this.EmpleadoId = request.EmpleadoId;
			cambios = true;
		}
		if (Estado != request.Estado)
		{
			this.Estado = request.Estado;
			cambios = true;
		}
		if (NotasAdicionales != request.NotasAdicionales)
		{
			this.NotasAdicionales = request.NotasAdicionales;
			cambios = true;
		}


		return cambios;
	}

	public ReservaResponse ToResponse()
	{
		return new ReservaResponse
		{
			Id = this.Id,
			Inicio = this.Inicio,
			Fin = this.Inicio.AddHours((double)this.Servicio.DuracionEstimada),
			ClienteId = this.Id,
			Cliente = new ClienteResponse()
			{
				Id = this.Cliente.Id,
				Nombre = this.Cliente.Nombre,
				Telefono = this.Cliente.Telefono,
				CorreoElectronico = this.Cliente.CorreoElectronico,
				Direcion = this.Cliente.Direcion
			},
			VehiculoId = this.Id,
			Vehiculo = new VehiculoResponse()
			{
				Id = this.Vehiculo.Id,
				Placa = this.Vehiculo.Placa,
				Marca = this.Vehiculo.Marca,
				Modelo = this.Vehiculo.Modelo,
				Color = this.Vehiculo.Color,
				Tipo = this.Vehiculo.Tipo,
				Cliente = new ClienteResponse()
				{
					Id = this.Cliente.Id,
					Nombre = this.Cliente.Nombre,
					Telefono = this.Cliente.Telefono,
					CorreoElectronico = this.Cliente.CorreoElectronico,
					Direcion = this.Cliente.Direcion
				}
			},
			ServicioId = this.Id,
			Servicio = new ServicioResponse()
			{
				Id = this.Servicio.Id,
				Nombre = this.Servicio.Nombre,
				Descripcion = this.Servicio.Descripcion,
				DuracionEstimada = this.Servicio.DuracionEstimada,
				Precio = this.Servicio.Precio
			},
			EmpleadoId = this.Id,
			Empleado = new EmpleadoResponse()
			{
				Id = this.Empleado.Id,
				Nombre = this.Empleado.Nombre,
				Apellido = this.Empleado.Apellido,
				Telefono = this.Empleado.Telefono,
				CorreoElectronico = this.Empleado.CorreoElectronico,
				InicioTrabajo = this.Empleado.InicioTrabajo,
				FinTrabajo = this.Empleado.FinTrabajo,
				Activo = this.Empleado.Activo
			},
			Estado = this.Estado,
			NotasAdicionales = this.NotasAdicionales
		};

	}

	#endregion

}