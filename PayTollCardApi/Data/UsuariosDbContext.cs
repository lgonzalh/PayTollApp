using Microsoft.EntityFrameworkCore;
using PayTollCardApi.Models;

namespace PayTollCardApi.Data
{
    public class UsuariosDbContext : DbContext
    {
        public UsuariosDbContext(DbContextOptions<UsuariosDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }

        // Otros DbSets como Tarjetas y CategoriasVehiculos
        public DbSet<Tarjeta> Tarjetas { get; set; }
        public DbSet<CategoriaVehiculo> CategoriasVehiculos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuraciones adicionales si son necesarias
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Cedula)
                .IsUnique();

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.CorreoElectronico)
                .IsUnique();
        }
    }
}
