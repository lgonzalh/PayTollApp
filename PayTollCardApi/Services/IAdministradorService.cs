using System.Collections.Generic;
using System.Threading.Tasks;
using PayTollCardApi.Models;

namespace PayTollCardApi.Services
{
    public interface IAdministradorService
    {
        // Gestión de Usuarios
        Task<List<Usuario>> GetAllUsuariosAsync();
        Task<Usuario?> GetUsuarioByCedulaAsync(string cedula);
        Task<Usuario> CreateUsuarioAsync(Usuario usuario);
        Task UpdateUsuarioByCedulaAsync(string cedula, Usuario usuario);
        Task DeleteUsuarioByCedulaAsync(string cedula);

        // Gestión de Tarjetas
        Task<List<Tarjeta>> GetTarjetasByCedulaAsync(string cedula);
        Task<Tarjeta> CreateTarjetaAsync(Tarjeta tarjeta);
        Task UpdateTarjetaAsync(Tarjeta tarjeta);
        Task DeleteTarjetaAsync(string numeroTarjeta);

        // Gestión de Recargas y Pagos
        Task<List<Recarga>> GetAllRecargasByCedulaAsync(string cedula);
        Task<List<Pago>> GetAllPagosByCedulaAsync(string cedula);

        // Gestión de Solicitudes
        Task<List<Solicitud>> GetAllSolicitudesByCedulaAsync(string cedula);
        Task<Solicitud> CreateSolicitudAsync(Solicitud solicitud);
        Task UpdateSolicitudAsync(Solicitud solicitud);
        Task DeleteSolicitudAsync(int idSolicitud);

        // Método adicional para Contactos (si aplica)
        Task<List<Contacto>> GetContactosByUsuarioIdAsync(int usuarioId);
    }
}
