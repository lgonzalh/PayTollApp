using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayTollApp.Models;
using UsuariosService.Data;

namespace UsuariosService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController(UsuariosDbContext context) : ControllerBase
    {
        private readonly UsuariosDbContext _context = context;

        // Método para registrar un nuevo usuario
        [HttpPost("register")]
        public async Task<IActionResult> Register(Usuario usuario)
        {
            try
            {
                // Verificar si el correo electrónico ya está registrado
                if (_context.Usuarios.Any(u => u.CorreoElectronico == usuario.CorreoElectronico))
                {
                    return BadRequest("El correo electrónico ya está registrado.");
                }

                // Agregar el nuevo usuario a la base de datos
                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();
                return Ok("Usuario registrado exitosamente");
            }
            catch (Exception ex)
            {
                // Capturar el error y devolver un mensaje más descriptivo
                return StatusCode(500, $"Error al registrar el usuario: {ex.Message}");
            }
        }

        // Método para iniciar sesión de un usuario
        [HttpPost("login")]
        public async Task<IActionResult> Login(string correo, string contrasena)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.CorreoElectronico == correo && u.Contrasena == contrasena);

            if (usuario == null)
                return Unauthorized("Credenciales incorrectas");

            return Ok("Login exitoso");
        }
    }
}
