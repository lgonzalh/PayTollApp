using Microsoft.EntityFrameworkCore;
using PayTollApp.Models;

namespace PayTollApp.DataAccess
{
    public class TarjetasDbContext : DbContext
    {
        public TarjetasDbContext(DbContextOptions<TarjetasDbContext> options) : base(options)
        {
        }

        public DbSet<Tarjeta> Tarjetas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Recarga> Recargas { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Tarjeta>().ToTable("TARJETAS", "SERVICIO");
            modelBuilder.Entity<Usuario>().ToTable("USUARIOS", "USUARIO");
            modelBuilder.Entity<Recarga>().ToTable("RECARGAS", "SERVICIO"); 
        }
    }
}
