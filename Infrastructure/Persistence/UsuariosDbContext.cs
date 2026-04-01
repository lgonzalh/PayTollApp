using Microsoft.EntityFrameworkCore;
using PayTollCardApi.Core.Entities;
using PayTollCardApi.Web.Models;

namespace PayTollCardApi.Infrastructure.Persistence
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
        public DbSet<Vehiculo> Vehiculos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>().ToTable("usuarios", "usuario");
            modelBuilder.Entity<Tarjeta>().ToTable("tarjetas", "servicio");
            modelBuilder.Entity<CategoriaVehiculo>().ToTable("categorias_vehiculos", "servicio");
            modelBuilder.Entity<Vehiculo>().ToTable("vehiculos", "servicio");

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

