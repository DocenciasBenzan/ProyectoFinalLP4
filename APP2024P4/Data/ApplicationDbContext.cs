using APP2024P4.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace APP2024P4.Data;

public interface IApplicationDbContext
{
    DbSet<Bill> Bills { get; set; }
    DbSet<Brand> Brands { get; set; }
    DbSet<Client> Clients { get; set; }
    DbSet<Engine> Engines { get; set; }
    DbSet<Model> Models { get; set; }
    DbSet<Payment> Payments { get; set; }
    DbSet<Vehicle> Vehicles { get; set; }
    DbSet<VehicleTransaction> VehicleTransactions { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options), IApplicationDbContext
{
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Model> Models { get; set; }
    public DbSet<Engine> Engines { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<Bill> Bills { get; set; }
    public DbSet<Client> Clients { get; set; }

    public DbSet<Payment> Payments { get; set; }

    public DbSet<VehicleTransaction> VehicleTransactions { get; set; }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(cancellationToken);
    }
}
