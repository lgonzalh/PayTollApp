using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayTollCardApi.Data;
using PayTollCardApi.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PayTollCardApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuariosDbContext _context;

        public UsuariosController(UsuariosDbContext context)
        {
            _context = context;
        }

        // Método para registrar un nuevo usuario
        [HttpPost("register")]
        [Consumes("application/json")]
        public async Task<IActionResult> Register([FromBody] Usuario usuario)
        {
            try
            {
                if (usuario == null)
                {
                    return BadRequest(new { Message = "Datos de usuario inválidos." });
                }

                if (string.IsNullOrWhiteSpace(usuario.Cedula))
                {
                    return BadRequest(new { Message = "La cédula es requerida." });
                }

                if (_context.Usuarios.Any(u => u.CorreoElectronico == usuario.CorreoElectronico))
                {
                    return BadRequest(new { Message = "El correo electrónico ya está registrado." });
                }

                if (_context.Usuarios.Any(u => u.Cedula == usuario.Cedula))
                {
                    return BadRequest(new { Message = "La cédula ya está registrada." });
                }

                usuario.FechaCreacion = DateTime.UtcNow;
                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Usuario registrado exitosamente." });
            }
            catch (DbUpdateException dbEx)
            {
                // Manejar excepciones específicas de la base de datos
                return StatusCode(500, new { Message = $"Error al registrar el usuario: {dbEx.InnerException?.Message ?? dbEx.Message}" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Error al registrar el usuario: {ex.Message}" });
            }
        }

        // Método para iniciar sesión de un usuario
        [HttpPost("login")]
        [Consumes("application/json")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.CorreoElectronico) || string.IsNullOrEmpty(model.Contrasena))
            {
                return BadRequest(new { Message = "Correo electrónico y contraseña son requeridos." });
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.CorreoElectronico == model.CorreoElectronico && u.Contrasena == model.Contrasena);

            if (usuario == null)
            {
                return Unauthorized(new { Message = "Credenciales incorrectas." });
            }

            return Ok(new
            {
                Message = "Inicio de sesión exitoso.",
                Usuario = new
                {
                    usuario.Id,
                    usuario.CorreoElectronico,
                    usuario.Nombre
                }
            });
        }

        // Método para obtener el perfil de un usuario (sin autorización por ahora)
        [HttpGet("perfil/{cedula}")]
        public async Task<IActionResult> ObtenerPerfil(string cedula)
        {
            if (string.IsNullOrWhiteSpace(cedula))
            {
                return BadRequest(new { Message = "Cédula es requerida." });
            }

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Cedula == cedula);
            if (usuario == null)
            {
                return NotFound(new { Message = "Usuario no encontrado." });
            }

            usuario.Contrasena = null; // Ocultar la contraseña en la respuesta
            return Ok(usuario);
        }

        // Método para obtener la categoría del vehículo de un usuario por su cédula
        [HttpGet("categoria/{cedula}")]
        public async Task<IActionResult> ObtenerCategoriaPorCedula(string cedula)
        {
            if (string.IsNullOrWhiteSpace(cedula))
            {
                return BadRequest(new { Message = "Cédula es requerida." });
            }

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Cedula == cedula);
            if (usuario == null)
            {
                return NotFound(new { Message = "Usuario no encontrado." });
            }

            var tarjeta = await _context.Tarjetas.FirstOrDefaultAsync(t => t.IdUsuario == usuario.Id);
            if (tarjeta == null)
            {
                return NotFound(new { Message = "Tarjeta no encontrada para el usuario." });
            }

            var categoria = await _context.CategoriasVehiculos.FirstOrDefaultAsync(c => c.IdCategoria == tarjeta.IdVehiculo);
            if (categoria == null)
            {
                return NotFound(new { Message = "Categoría de vehículo no encontrada." });
            }

            return Ok(categoria);
        }
    }

    // Clase auxiliar para el modelo de inicio de sesión
    public class LoginModel
    {
        public string CorreoElectronico { get; set; }
        public string Contrasena { get; set; }
    }
}
