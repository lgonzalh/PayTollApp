using Microsoft.EntityFrameworkCore;
using PayTollCardApi.Models;

namespace PayTollCardApi.Data
{
    public class VehiculosDbContext(DbContextOptions<VehiculosDbContext> options) : DbContext(options)
    {
        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }  // Agregar el DbSet de Usuarios

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Vehiculo>().ToTable("VEHICULOS", "SERVICIO");
            modelBuilder.Entity<Usuario>().ToTable("USUARIOS", "USUARIO");
        }
    }
}
