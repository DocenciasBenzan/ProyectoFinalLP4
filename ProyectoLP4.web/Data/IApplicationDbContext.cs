using Microsoft.EntityFrameworkCore;
using ProyectoLP4.web.Models;

namespace ProyectoLP4.web.Data
{
    public interface IApplicationDbContext
    {
        DbSet<UserList> UserLists { set; get; }
        DbSet<Movie> Movies { set; get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}