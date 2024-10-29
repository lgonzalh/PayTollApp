using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayTollApp.DataAccess;
using PayTollApp.Models;
using SolicitudesService.Models;

namespace SolicitudesService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SolicitudesController : ControllerBase
    {
        private readonly TarjetasDbContext _context;

        public SolicitudesController(TarjetasDbContext context)
        {
            _context = context;
        }

        // Endpoint para crear una solicitud
        [HttpPost("crear")]
        public async Task<IActionResult> CrearSolicitud([FromBody] SolicitudRequest solicitudRequest)
        {
            try
            {
                // Validar que el usuario exista
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Cedula == solicitudRequest.Cedula);
                if (usuario == null)
                {
                    return NotFound("El usuario no existe.");
                }

                // Crear la solicitud
                var solicitud = new Solicitud
                {
                    IdUsuario = usuario.Id,
                    TipoSolicitud = solicitudRequest.TipoSolicitud,
                    Descripcion = solicitudRequest.Descripcion,
                    FechaSolicitud = DateTime.Now,
                    Estado = "Pendiente" // Estado inicial
                };

                // Registrar la solicitud
                _context.Solicitudes.Add(solicitud);

                // Guardar cambios en la base de datos
                await _context.SaveChangesAsync();

                // Crear el objeto de respuesta
                var solicitudDto = new
                {
                    Mensaje = "Solicitud creada exitosamente.",
                    Solicitud = new
                    {
                        solicitud.IdSolicitud,
                        solicitud.TipoSolicitud,
                        solicitud.Descripcion,
                        solicitud.FechaSolicitud,
                        solicitud.Estado
                    }
                };

                return Ok(solicitudDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear la solicitud: {ex.Message}");
            }
        }

        // Endpoint para obtener el historial de solicitudes usando la cédula
        [HttpGet("historial/{cedula}")]
        public async Task<IActionResult> Historial(string cedula)
        {
            try
            {
                // Validar que el usuario exista
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Cedula == cedula);
                if (usuario == null)
                {
                    return NotFound("El usuario no existe.");
                }

                // Obtener las solicitudes del usuario
                var solicitudes = await _context.Solicitudes
                    .Where(s => s.IdUsuario == usuario.Id)
                    .OrderByDescending(s => s.FechaSolicitud)
                    .ToListAsync();

                if (!solicitudes.Any())
                {
                    return NotFound("No se encontraron solicitudes para el usuario proporcionado.");
                }

                // Mapear las solicitudes a DTOs
                var solicitudesDto = solicitudes.Select(s => new
                {
                    s.IdSolicitud,
                    s.TipoSolicitud,
                    s.Descripcion,
                    s.FechaSolicitud,
                    s.Estado
                });

                return Ok(solicitudesDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener el historial de solicitudes: {ex.Message}");
            }
        }
    }
}
