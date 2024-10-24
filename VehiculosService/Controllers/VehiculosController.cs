using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayTollApp.Models;
using PayTollApp.SharedServices;
using VehiculosService.Data;

namespace VehiculosService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehiculosController(VehiculosDbContext context, TarjetaService tarjetaService) : ControllerBase
    {
        private readonly VehiculosDbContext _context = context;
        private readonly TarjetaService _tarjetaService = tarjetaService;

        [HttpPost("register")]
        public async Task<IActionResult> Register(Vehiculo vehiculo)
        {
            if (_context.Vehiculos.Any(v => v.Placa == vehiculo.Placa))
            {
                return BadRequest("La placa ya está registrada.");
            }

            try
            {
                _context.Vehiculos.Add(vehiculo);
                await _context.SaveChangesAsync();

                await _tarjetaService.CrearTarjetaParaVehiculoAsync(vehiculo.IdUsuario, vehiculo.Id);

                return Ok("Vehículo registrado exitosamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al registrar el vehículo: {ex.Message}");
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

            var vehiculos = await _context.Vehiculos
                .Where(v => v.IdUsuario == usuario.Id)
                .ToListAsync();

            if (vehiculos.Count == 0)
            {
                return NotFound("No se encontraron vehículos para el usuario con la cédula proporcionada.");
            }

            return Ok(vehiculos);
        }
    }
}
