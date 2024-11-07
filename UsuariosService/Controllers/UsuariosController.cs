using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayTollApp.Models;
using UsuariosService.Data;
using System;
using System.Threading.Tasks;

namespace UsuariosService.Controllers
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
        public async Task<IActionResult> Register([FromBody] Usuario usuario)
        {
            try
            {
                if (_context.Usuarios.Any(u => u.CorreoElectronico == usuario.CorreoElectronico))
                {
                    return BadRequest("El correo electrónico ya está registrado.");
                }

                usuario.FechaCreacion = DateTime.Now;
                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();
                return Ok("Usuario registrado exitosamente");
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage += $" Inner Exception: {ex.InnerException.Message}";
                }
                return StatusCode(500, $"Error al registrar el usuario: {errorMessage}");
            }
        }

        // Método para iniciar sesión de un usuario
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.CorreoElectronico == model.CorreoElectronico && u.Contrasena == model.Contrasena);

            if (usuario == null)
                return Unauthorized("Credenciales incorrectas");

            return Ok("Login exitoso");
        }

        // Método para obtener el perfil sin autorización
        [HttpGet("perfil")]
        public async Task<IActionResult> ObtenerPerfil()
        {
            // Temporalmente devolver un perfil de usuario de prueba
            var usuario = await _context.Usuarios.FirstOrDefaultAsync();
            if (usuario == null)
            {
                return NotFound("Usuario no encontrado.");
            }

            usuario.Contrasena = null; // Para evitar devolver la contraseña
            return Ok(usuario);
        }
    }

    // Modelo de login simplificado
    public class LoginModel
    {
        public string? CorreoElectronico { get; set; }
        public string? Contrasena { get; set; }
    }
}
