using APP2024P4.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace APP2024P4.Data
{
    public interface IApplicationDbContext
    {
        DbSet<Producto> Productos { set; get; }
        DbSet<Categoria> Categorias { set; get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}