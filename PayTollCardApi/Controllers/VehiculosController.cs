using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayTollCardApi.Models;
using PayTollCardApi.Data;

namespace VehiculosService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehiculosController : ControllerBase
    {
        private readonly VehiculosDbContext _context;

        public VehiculosController(VehiculosDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(VehiculoRegistroDto vehiculoDto)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Cedula == vehiculoDto.Cedula);

            if (usuario == null)
            {
                return NotFound("El usuario no existe para registrar el vehículo.");
            }

            if (_context.Vehiculos.Any(v => v.Placa == vehiculoDto.Placa))
            {
                return BadRequest("La placa ya está registrada.");
            }

            var vehiculo = new Vehiculo
            {
                IdUsuario = usuario.Id,
                Placa = vehiculoDto.Placa,
                CategoriaVehiculo = vehiculoDto.CategoriaVehiculo
            };

            try
            {
                _context.Vehiculos.Add(vehiculo);
                await _context.SaveChangesAsync();
                return Ok("Vehículo registrado exitosamente.");
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
                return NotFound("Usuario no encontrado.");
            }

            var vehiculos = await _context.Vehiculos
                .Where(v => v.IdUsuario == usuario.Id)
                .ToListAsync();

            return Ok(vehiculos);
        }


    }
}
