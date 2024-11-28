using APP2024P4.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace APP2024P4.Data.dbcontext;

public interface IDatabaseApp
{
    DbSet<Categoria> Categorias { get; set; }
    DbSet<Producto> Productos { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

