using APP2024P4.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskMaster;

namespace APP2024P4.Service;

public interface IUserService
{
    Task<ResultList<string>> GetAllUserEmailsAsync();
}
public partial class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ResultList<string>> GetAllUserEmailsAsync()
    {
        try
        {
            List<string?> list = await _userManager.Users
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
