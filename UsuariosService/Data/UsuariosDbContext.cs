using Microsoft.EntityFrameworkCore;
using PayTollApp.Models;

namespace UsuariosService.Data
{
    public class UsuariosDbContext : DbContext
    {
        public UsuariosDbContext(DbContextOptions<UsuariosDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Tarjeta> Tarjetas { get; set; }
        public DbSet<CategoriaVehiculo> CategoriasVehiculos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().ToTable("USUARIOS", schema: "USUARIO");
            modelBuilder.Entity<Tarjeta>().ToTable("TARJETAS", schema: "SERVICIO");
            modelBuilder.Entity<CategoriaVehiculo>().ToTable("CATEGORIAS_VEHICULOS", schema: "SERVICIO");
        }
    }
}
