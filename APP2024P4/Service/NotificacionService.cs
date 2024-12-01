using APP2024P4.Data.Dtos;
using APP2024P4.Data;
using Microsoft.AspNetCore.Identity;
using TaskMaster;
using APP2024P4.Data.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
namespace APP2024P4.Service;
public interface INotificationService
{
    Task<Result> SendNotificationAsync(NotifiacioRequest notificacion);
    Task<ResultList<NotificacionDto>> GetNotificacionByEmail(string renderEmail);
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
    public async Task<Result> SendNotificationAsync(NotifiacioRequest notificacion)
    {
        try
        {
            var entity = Notificacion.Create(
                notificacion.UserId,
                notificacion.SenderEmail,
                notificacion.RenderEmail,
                notificacion.Message,
                notificacion.Isread,
                notificacion.FechaCreacion,
                notificacion.TareaId

                );
            _context.Notificaciones.Add(entity);
            await _context.SaveChangesAsync();

            return Result.Success("✅Notificaion registrado con éxito!");
        }
        catch (Exception Ex)
        {
            return Result.Failure($"☠️ Error: {Ex.Message}");
        }
    }
    public async Task<ResultList<NotificacionDto>> GetNotificacionByEmail(string renderEmail)
    {
        try
        {
            var entity = await _context.Notificaciones.Where(p => p.RenderEmail == renderEmail)
                .Select(p => new NotificacionDto(
                    p.Id,
                    p.UserId,
                    p.SenderEmail,
                    p.RenderEmail,
                    p.Message,
                    p.TareaId,
                    p.Tareas!.Titulo ?? "No definido",
                    p.Isread,
                    p.FechaCreacion))
                .ToListAsync();
            if (entity == null)
                return ResultList<NotificacionDto>.Failure($"El producto no existe!");

            return ResultList<NotificacionDto>.Success(entity);
        }
        catch (Exception Ex)
        {
            return ResultList<NotificacionDto>.Failure($"☠️ Error: {Ex.Message}");
        }
    }
    public async Task<Result> RespondToInvitationAsync(int notificationId, bool isAccepted ,string userId)
    {
        try
        {
            var notification = await _context.Notificaciones.FindAsync(notificationId);
            if (notification == null)
            {
                return Result.Failure("Notificación no encontrada.");
            }
            if (isAccepted)
            {
                notification.Status = "Accepted";

                // Agregar al colaborador
                var colaboradorRequest = new ColaboradorRequest
                {
                    CreadorEmail = notification.SenderEmail,
                    ColaboradorEmail = notification.RenderEmail,
                    IsApproved = true,
                    UserId = userId
                };
                var addResult = await _colaboradorService.Addcolaborador(colaboradorRequest);
                if (!addResult.Succesd)
                {
                    Result.Success(addResult.Message);
                }
            }
            else
            {
                notification.Status = "Rejected";
            }
            await _context.SaveChangesAsync();
            return Result.Success("Respuesta a la invitación procesada con éxito.");
        }
        catch (Exception ex)
        {
            return Result.Failure($"☠️ Error: {ex.Message}");
        }
    }

}
