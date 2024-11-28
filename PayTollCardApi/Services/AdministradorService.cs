using Microsoft.EntityFrameworkCore;
using PayTollCardApi.DataAccess;
using PayTollCardApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PayTollCardApi.Services
{
    public class AdministradorService : IAdministradorService
    {
        private readonly TarjetasDbContext _context;

        public AdministradorService(TarjetasDbContext context)
        {
            _context = context;
        }

        // Gestión de Usuarios
        public async Task<List<Usuario>> GetAllUsuariosAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<Usuario?> GetUsuarioByCedulaAsync(string cedula)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Cedula == cedula);
        }

        public async Task<Usuario> CreateUsuarioAsync(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task UpdateUsuarioByCedulaAsync(string cedula, Usuario usuario)
        {
            var existingUser = await GetUsuarioByCedulaAsync(cedula);
            if (existingUser != null)
            {
                existingUser.Nombre = usuario.Nombre;
                existingUser.CorreoElectronico = usuario.CorreoElectronico;
                existingUser.Contrasena = usuario.Contrasena;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteUsuarioByCedulaAsync(string cedula)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Cedula == cedula);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }
        }

        // Gestión de Tarjetas
        public async Task<List<Tarjeta>> GetTarjetasByCedulaAsync(string cedula)
        {
            var usuario = await GetUsuarioByCedulaAsync(cedula);
            return usuario == null ? new List<Tarjeta>() : await _context.Tarjetas.Where(t => t.IdUsuario == usuario.Id).ToListAsync();
        }

        public async Task<Tarjeta> CreateTarjetaAsync(Tarjeta tarjeta)
        {
            _context.Tarjetas.Add(tarjeta);
            await _context.SaveChangesAsync();
            return tarjeta;
        }

        public async Task UpdateTarjetaAsync(Tarjeta tarjeta)
        {
            _context.Entry(tarjeta).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTarjetaAsync(string numeroTarjeta)
        {
            var tarjeta = await _context.Tarjetas.FirstOrDefaultAsync(t => t.NumeroTarjeta == numeroTarjeta);
            if (tarjeta != null)
            {
                _context.Tarjetas.Remove(tarjeta);
                await _context.SaveChangesAsync();
            }
        }

        // Gestión de Recargas y Pagos
        public async Task<List<Recarga>> GetAllRecargasByCedulaAsync(string cedula)
        {
            var usuario = await GetUsuarioByCedulaAsync(cedula);
            return usuario == null ? new List<Recarga>() : await _context.Recargas
                .Include(r => r.Tarjeta)
                .Where(r => r.Tarjeta.IdUsuario == usuario.Id)
                .ToListAsync();
        }

        public async Task<List<Pago>> GetAllPagosByCedulaAsync(string cedula)
        {
            var usuario = await GetUsuarioByCedulaAsync(cedula);
            return usuario == null ? new List<Pago>() : await _context.Pagos
                .Include(p => p.Tarjeta)
                .Where(p => p.IdUsuario == usuario.Id)
                .ToListAsync();
        }

        // Gestión de Solicitudes
        public async Task<List<Solicitud>> GetAllSolicitudesByCedulaAsync(string cedula)
        {
            var usuario = await GetUsuarioByCedulaAsync(cedula);
            return usuario == null ? new List<Solicitud>() : await _context.Solicitudes.Where(s => s.IdUsuario == usuario.Id).ToListAsync();
        }

        public async Task<Solicitud> CreateSolicitudAsync(Solicitud solicitud)
        {
            _context.Solicitudes.Add(solicitud);
            await _context.SaveChangesAsync();
            return solicitud;
        }

        public async Task UpdateSolicitudAsync(Solicitud solicitud)
        {
            _context.Entry(solicitud).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSolicitudAsync(int idSolicitud)
        {
            var solicitud = await _context.Solicitudes.FindAsync(idSolicitud);
            if (solicitud != null)
            {
                _context.Solicitudes.Remove(solicitud);
                await _context.SaveChangesAsync();
            }
        }

        // Método adicional para Contactos (si aplica)
        public async Task<List<Contacto>> GetContactosByUsuarioIdAsync(int usuarioId)
        {
            return await _context.Contactos.Where(c => c.IdUsuario == usuarioId).ToListAsync();
        }
    }
}
