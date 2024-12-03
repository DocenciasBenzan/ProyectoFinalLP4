using APP2024P4.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskMaster;

namespace APP2024P4.Service;

public interface IUserService
{
    Task<ResultList<string>> GetAllUserEmailsAsync(string currentUserEmail);
}
public partial class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IApplicationDbContext _context;

    public UserService(UserManager<ApplicationUser> userManager, IApplicationDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public async Task<ResultList<string>> GetAllUserEmailsAsync(string currentUserEmail)
    {
        try
        {
            List<string?> list = await _userManager.Users
                .Where(u => u.Email != currentUserEmail) // Excluir el correo del usuario actual
                .Select(u => u.Email)
                .ToListAsync();

            return ResultList<string>.Success(list!);
        }
        catch (Exception Ex)
        {
            return ResultList<string>.Failure($"☠️ Error: {Ex.Message}");
        }
    }
}
