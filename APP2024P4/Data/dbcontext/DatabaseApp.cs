using APP2024P4.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace APP2024P4.Data.dbcontext;

public class DatabaseApp :DbContext, IDatabaseApp
{
    private readonly IConfiguration confi;

    public DatabaseApp(IConfiguration confi)
    {
        this.confi = confi;
    }

    public DbSet<Producto> Productos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(confi.GetConnectionString("MSSQL"));
    }
}
