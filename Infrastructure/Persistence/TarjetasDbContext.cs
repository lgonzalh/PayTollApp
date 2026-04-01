using Microsoft.EntityFrameworkCore;
using PayTollCardApi.Core.Entities;
using PayTollCardApi.Web.Models;

namespace PayTollCardApi.Infrastructure.Persistence
{
    public class TarjetasDbContext : DbContext
    {
        public TarjetasDbContext(DbContextOptions<TarjetasDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Tarjeta> Tarjetas { get; set; }
        public DbSet<Solicitud> Solicitudes { get; set; }
        public DbSet<Contacto> Contactos { get; set; }
        public DbSet<Peaje> Peajes { get; set; }
        public DbSet<CategoriaVehiculo> CategoriasVehiculos { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<Recarga> Recargas { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<Movimiento> Movimientos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Solicitud>()
                .HasKey(s => s.IdSolicitud);

            modelBuilder.Entity<Solicitud>()
                .HasOne(s => s.Usuario)
                .WithMany()
                .HasForeignKey(s => s.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Contacto>()
                .HasOne(c => c.Usuario)
                .WithMany()
                .HasForeignKey(c => c.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Tarjeta>()
                .HasOne(t => t.Usuario)
                .WithMany()
                .HasForeignKey(t => t.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Recarga>()
                .HasOne(r => r.Tarjeta)
                .WithMany()
                .HasForeignKey(r => r.IdTarjeta)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Pago>()
                .HasOne(p => p.Tarjeta)
                .WithMany()
                .HasForeignKey(p => p.IdTarjeta)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

