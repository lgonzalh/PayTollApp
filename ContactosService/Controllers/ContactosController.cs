using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayTollApp.DataAccess;
using PayTollApp.Models;
using System.Diagnostics.Contracts;

namespace ContactosService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactosController : ControllerBase
    {
        private readonly TarjetasDbContext _context;

        public ContactosController(TarjetasDbContext context)
        {
            _context = context;
        }

        [HttpPost("enviar")]
        public async Task<IActionResult> EnviarContacto([FromBody] Contacto contacto)
        {
            try
            {
                var usuarioExistente = await _context.Usuarios.FirstOrDefaultAsync(u => u.Cedula == contacto.Usuario.Cedula);
                if (usuarioExistente != null)
                {
                    contacto.IdUsuario = usuarioExistente.Id;
                    contacto.Usuario = null; // Evitar reinsertar usuario
                }

                _context.Contactos.Add(contacto);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    Mensaje = "Mensaje enviado exitosamente.",
                    Contacto = contacto
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al enviar el mensaje: {ex.Message}");
            }
        }

        [HttpGet("historial/{cedula}")]
        public async Task<IActionResult> Historial(string cedula)
        {
            try
            {
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Cedula == cedula);
                if (usuario == null) return NotFound("El usuario no existe.");

                var contactos = await _context.Contactos
                    .Where(c => c.IdUsuario == usuario.Id)
                    .OrderByDescending(c => c.FechaContacto)
                    .ToListAsync();

                return Ok(contactos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener el historial: {ex.Message}");
            }
        }
    }
}
