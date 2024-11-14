using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UsuariosService.Data;
using PayTollApp.Models;

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


        // Otros métodos como Login, Register, etc.
    }
}
