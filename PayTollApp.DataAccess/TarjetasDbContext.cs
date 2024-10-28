using Microsoft.EntityFrameworkCore;
using PayTollApp.Models;

namespace PayTollApp.DataAccess
{
    public class TarjetasDbContext : DbContext
    {
        public TarjetasDbContext(DbContextOptions<TarjetasDbContext> options) : base(options) { }

        public DbSet<Recarga> Recargas { get; set; }
        public DbSet<Tarjeta> Tarjetas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<Peaje> Peajes { get; set; }
        public DbSet<CategoriaVehiculo> CategoriasVehiculos { get; set; }
        public DbSet<Movimiento> Movimientos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Recarga>().ToTable("RECARGAS", "SERVICIO");
            modelBuilder.Entity<Tarjeta>().ToTable("TARJETAS", "SERVICIO");
            modelBuilder.Entity<Usuario>().ToTable("USUARIOS", "USUARIO");
            modelBuilder.Entity<Pago>().ToTable("PAGOS", "SERVICIO");
            modelBuilder.Entity<Peaje>().ToTable("PEAJES", "SERVICIO");
            modelBuilder.Entity<CategoriaVehiculo>().ToTable("CATEGORIAS_VEHICULOS", "SERVICIO");
        }
    }
}
