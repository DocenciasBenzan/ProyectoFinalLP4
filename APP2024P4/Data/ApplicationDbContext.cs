using APP2024P4.Data.Entidades;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace APP2024P4.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options), IApplicationDbContext
    {
        public DbSet<Tarea> Tareas { get; set; }
        public DbSet<Colaborador> Colaboradores { get; set; }
        public DbSet<Notificacion> Notificaciones { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Colaborador>()
                .HasOne(c => c.Tareas)
                .WithMany(t => t.Colaboradores)
                .HasForeignKey(c => c.TareaId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Colaborador>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comentario>()
                .HasOne(c => c.Tareas)
                .WithMany(t => t.Comentarios)
                .HasForeignKey(c => c.TareaId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Comentario>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<Notificacion>()
                .HasOne(n => n.Tareas)
                .WithMany(t => t.Notificaciones)
                .HasForeignKey(n => n.TareaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Notificacion>()
                .HasOne(n => n.User)
                .WithMany()
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        
        }

    }
}
