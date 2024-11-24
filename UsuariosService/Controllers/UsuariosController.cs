using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UsuariosService.Data;
using PayTollApp.Models;
using System;
using System.Linq;
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

                return Ok("Usuario registrado exitosamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al registrar el usuario: {ex.Message}");
            }
        }

        // Método para iniciar sesión de un usuario
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.CorreoElectronico == model.CorreoElectronico && u.Contrasena == model.Contrasena);

            if (usuario == null)
            {
                return Unauthorized("Credenciales incorrectas.");
            }

            return Ok("Inicio de sesión exitoso.");
        }

        // Método para obtener el perfil de un usuario (sin autorización por ahora)
        [HttpGet("perfil/{cedula}")]
        public async Task<IActionResult> ObtenerPerfil(string cedula)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Cedula == cedula);
            if (usuario == null)
            {
                return NotFound("Usuario no encontrado.");
            }

            usuario.Contrasena = null; // Ocultar la contraseña en la respuesta
            return Ok(usuario);
        }

        // Método para obtener la categoría del vehículo de un usuario por su cédula
        [HttpGet("categoria/{cedula}")]
        public async Task<IActionResult> ObtenerCategoriaPorCedula(string cedula)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Cedula == cedula);
            if (usuario == null)
            {
                return NotFound("Usuario no encontrado.");
            }

            var tarjeta = await _context.Tarjetas.FirstOrDefaultAsync(t => t.IdUsuario == usuario.Id);
            if (tarjeta == null)
            {
                return NotFound("Tarjeta no encontrada para el usuario.");
            }

            var categoria = await _context.CategoriasVehiculos.FirstOrDefaultAsync(c => c.IdCategoria == tarjeta.IdVehiculo);
            if (categoria == null)
            {
                return NotFound("Categoría de vehículo no encontrada.");
            }

            return Ok(categoria);
        }
    }

    // Clase auxiliar para el modelo de inicio de sesión
    public class LoginModel
    {
        public string? CorreoElectronico { get; set; }
        public string? Contrasena { get; set; }
    }
}
