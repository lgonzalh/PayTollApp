using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayTollCardApi.Core.Entities;
using PayTollCardApi.Web.Models;
using PayTollCardApi.Infrastructure.Persistence;

namespace PayTollCardApi.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehiculosController : ControllerBase
    {
        private readonly VehiculosDbContext _context;
        private readonly PayTollCardApi.Core.Services.TarjetaService _tarjetaService;

        public VehiculosController(VehiculosDbContext context, PayTollCardApi.Core.Services.TarjetaService tarjetaService)
        {
            _context = context;
            _tarjetaService = tarjetaService;
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

                // Crear automáticamente una tarjeta para el vehículo registrado
                await _tarjetaService.CrearTarjetaParaVehiculoAsync(usuario.Id, vehiculo.Id);

                return Ok("Vehículo registrado y tarjeta creada exitosamente.");
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

