using Microsoft.EntityFrameworkCore;
using PayTollApp.Models;

namespace UsuariosService.Data
{
    public class UsuariosDbContext(DbContextOptions<UsuariosDbContext> options) : DbContext(options)
    {
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>().ToTable("USUARIOS", "USUARIO");
        }
    }
}
