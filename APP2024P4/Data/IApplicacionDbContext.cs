using Microsoft.EntityFrameworkCore;
using APP2024P4.Data.Entities;

namespace APP2024P4.Data
{
    public class IApplicacionDbContext
    {
        DbSet<Producto> Productos { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
