﻿using APP2024P4.Data.Dtos;
using APP2024P4.Data;
using Microsoft.AspNetCore.Identity;
using TaskMaster;
using APP2024P4.Data.Entidades;
using Microsoft.EntityFrameworkCore;
namespace APP2024P4.Service;
public interface INotificationService
{
    Task<Result> SendNotificationAsync(NotifiacioRequest notificacion, string userId, int tareaId, string renderEmail);
    Task<ResultList<NotificacionDto>> GetNotificacionByEmail(string renderEmail, bool Isread);
    Task<Result> RespondToInvitationAsync(int notificationId, bool isAccepted, string userId, int tareaId);
    Task<Result> Delete(int Id);
}
public partial class NotificationService : INotificationService
{
    private readonly IApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IColaboradorService _colaboradorService;
    public NotificationService(
        IApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        IColaboradorService colaboradorService)
    {
        _context = context;
        _userManager = userManager;
        _colaboradorService = colaboradorService;
    }
    public async Task<Result> SendNotificationAsync(NotifiacioRequest notificacion, string userId, int tareaId, string renderEmail)
    {
        try
        {
            // Verifica si ya existe una notificación para este usuario y tarea
            var existingNotification = await _context.Notificaciones
                .FirstOrDefaultAsync(n =>
                    n.UserId == userId &&
                    n.TareaId == tareaId &&
                    n.RenderEmail == renderEmail);

            // Si ya existe una notificación y fue gestionada, no envía otra
            if (existingNotification != null)
            {
                if (existingNotification.Isread || existingNotification.Status == "Accepted")
                {
                    return Result.Failure("⚠️ El usuario ya recibió y gestionó esta invitación.");
                }

                // Si existe pero no fue gestionada, se actualiza la notificación
                existingNotification.Message = notificacion.Message;
                existingNotification.Isread = notificacion.Isread;
                existingNotification.FechaCreacion = DateTime.UtcNow;

                await _context.SaveChangesAsync();
                return Result.Success("✅ Notificación actualizada con éxito.");
            }

            // Si no existe, crea una nueva notificación
            var entity = Notificacion.Create(
                notificacion.UserId,
                notificacion.SenderEmail,
                notificacion.RenderEmail,
                notificacion.Message,
                notificacion.Isread,
                notificacion.FechaCreacion,
                notificacion.TareaId
            );
            entity.UserId = userId;

            _context.Notificaciones.Add(entity);
            await _context.SaveChangesAsync();

            return Result.Success("✅ Notificación registrada con éxito!");
        }
        catch (Exception Ex)
        {
            return Result.Failure($"☠️ Error: {Ex.Message}");
        }
    }

    public async Task<ResultList<NotificacionDto>> GetNotificacionByEmail(string renderEmail, bool isread)
    {
        try
        {
            // Filtra las notificaciones por correo y estado leído
            var entity = await _context.Notificaciones
                .Where(n => n.RenderEmail == renderEmail && n.Isread == isread)
                .Select(n => new NotificacionDto(
                    n.Id,
                    n.UserId,
                    n.SenderEmail,
                    n.RenderEmail,
                    n.Message,
                    n.TareaId,
                    n.Tareas!.Titulo ?? "No definido",
                    n.Isread,
                    n.FechaCreacion))
                .ToListAsync();

            // Valida si hay resultados
            if (entity == null || !entity.Any())
                return ResultList<NotificacionDto>.Failure("No se encontraron notificaciones.");

            return ResultList<NotificacionDto>.Success(entity);
        }
        catch (Exception ex)
        {
            return ResultList<NotificacionDto>.Failure($"☠️ Error: {ex.Message}");
        }
    }
    public async Task<Result> RespondToInvitationAsync(int notificationId, bool isAccepted, string userId, int tareaId)
    {
        try
        {
            // Busca la notificación por su ID
            var notification = await _context.Notificaciones.FindAsync(notificationId);
            if (notification == null)
            {
                return Result.Failure("Notificación no encontrada.");
            }

            // Verifica si ya se gestionó la invitación
            if (notification.Status == "Accepted" || notification.Status == "Rejected")
            {
                return Result.Failure("La invitación ya fue gestionada previamente.");
            }

            if (isAccepted)
            {
                // Aceptar la invitación
                notification.Status = "Accepted";
                notification.Isread = true; // Marca como leída

                // Crear solicitud para agregar al colaborador
                var colaboradorRequest = new ColaboradorRequest
                {
                    CreadorEmail = notification.SenderEmail,
                    ColaboradorEmail = notification.RenderEmail,
                    IsApproved = true,
                    UserId = userId,
                    TareaId = tareaId,
                };

                // Intenta agregar al colaborador
                var addResult = await _colaboradorService.Addcolaborador(colaboradorRequest);
                if (!addResult.Succesd)
                {
                    return Result.Failure(addResult.Message);
                }
            }
            else
            {
                // Rechazar la invitación
                notification.Status = "Rejected";
                notification.Isread = true; // Marca como leída
            }

            // Guarda los cambios en la base de datos
            await _context.SaveChangesAsync();
            return Result.Success("Respuesta a la invitación procesada con éxito.");
        }
        catch (Exception ex)
        {
            return Result.Failure($"☠️ Error: {ex.Message}");
        }
    }

    // Método para eliminar una notificación por su ID
    public async Task<Result> Delete(int Id)
    {
        try
        {
            var entity = _context.Notificaciones.Where(p => p.Id == Id).FirstOrDefault();
            if (entity == null)
                return Result.Failure($"La Notificacion '{Id}' no existe!");
            _context.Notificaciones.Remove(entity);
            await _context.SaveChangesAsync();
            return Result.Success("Notificacion eliminada con exito!");
        }
        catch (Exception Ex)
        {
            return Result.Failure($"Error: {Ex.Message}");
        }
    }

}
