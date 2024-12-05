using APP2024P4.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace APP2024P4.Data
{
    public interface IApplicationDbContext
    {
        DbSet<Producto> Productos { get; set; }
        DbSet<Categoria> Categorias { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}