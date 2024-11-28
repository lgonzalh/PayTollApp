using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayTollCardApi.Models;
using PayTollCardApi.DataAccess;

namespace TarjetasService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TarjetasController : ControllerBase
    {
        private readonly TarjetasDbContext _context;

        public TarjetasController(TarjetasDbContext context)
        {
            _context = context;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(Tarjeta tarjeta)
        {
            try
            {
                _context.Tarjetas.Add(tarjeta);
                await _context.SaveChangesAsync();
                return Ok("Tarjeta creada exitosamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear la tarjeta: {ex.Message}");
            }
        }

        [HttpGet("getByCedula/{cedula}")]
        public async Task<IActionResult> GetByCedula(string cedula)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Cedula == cedula);

            if (usuario == null)
            {
                return NotFound("No se encontró un usuario con la cédula proporcionada.");
            }

            var tarjetasList = await _context.Tarjetas
                .Where(t => t.IdUsuario == usuario.Id)
                .ToListAsync();

            if (tarjetasList.Count == 0)
            {
                return NotFound("No se encontraron tarjetas para el usuario con la cédula proporcionada.");
            }

            return Ok(tarjetasList);
        }
    }
}
