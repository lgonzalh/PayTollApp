using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayTollApp.DataAccess;
using PayTollApp.Models;
using RecargasService.Models; // Asegúrate de tener este using si RecargaRequest está en este namespace

namespace RecargasService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecargasController : ControllerBase
    {
        private readonly TarjetasDbContext _context;

        public RecargasController(TarjetasDbContext context)
        {
            _context = context;
        }

        // Endpoint para realizar una recarga usando la cédula
        [HttpPost("recargar")]
        public async Task<IActionResult> Recargar([FromBody] RecargaRequest recargaRequest)
        {
            try
            {
                // Validar que el usuario exista
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Cedula == recargaRequest.Cedula);
                if (usuario == null)
                {
                    return NotFound("El usuario no existe.");
                }

                // Obtener la(s) tarjeta(s) asociada(s) al usuario
                var tarjetas = await _context.Tarjetas.Where(t => t.IdUsuario == usuario.Id).ToListAsync();
                if (!tarjetas.Any())
                {
                    return NotFound("El usuario no tiene tarjetas asociadas.");
                }

                // Por simplicidad, asumamos que el usuario tiene una sola tarjeta
                var tarjeta = tarjetas.FirstOrDefault();

                // Validar el monto de la recarga
                if (recargaRequest.Monto <= 0)
                {
                    return BadRequest("El monto de la recarga debe ser mayor que cero.");
                }

                // Actualizar el saldo de la tarjeta
                tarjeta.Saldo += recargaRequest.Monto;

                // Crear la recarga
                var recarga = new Recarga
                {
                    IdTarjeta = tarjeta.Id,
                    Monto = recargaRequest.Monto,
                    FechaRecarga = DateTime.Now,
                    MetodoPago = recargaRequest.MetodoPago
                };

                // Registrar la recarga
                _context.Recargas.Add(recarga);

                // Guardar cambios en la base de datos
                await _context.SaveChangesAsync();

                // Crear el objeto de respuesta con la información enmascarada
                var recargaDto = new
                {
                    Mensaje = "Recarga realizada exitosamente.",
                    Recarga = new
                    {
                        recarga.Id,
                        recarga.Monto,
                        recarga.FechaRecarga,
                        recarga.MetodoPago
                    },
                    Tarjeta = new TarjetaDto
                    {
                        Id = tarjeta.Id,
                        Saldo = tarjeta.Saldo,
                        FechaCreacion = tarjeta.FechaCreacion,
                        NumeroTarjeta = tarjeta.NumeroTarjetaEnmascarado,
                        IdVehiculo = tarjeta.IdVehiculo
                    }
                };

                return Ok(recargaDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al realizar la recarga: {ex.Message}");
            }
        }

        // Endpoint para obtener el historial de recargas usando la cédula
        [HttpGet("historial/{cedula}")]
        public async Task<IActionResult> Historial(string cedula)
        {
            try
            {
                // Validar que el usuario exista
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Cedula == cedula);
                if (usuario == null)
                {
                    return NotFound("El usuario no existe.");
                }

                // Obtener la(s) tarjeta(s) asociada(s) al usuario
                var tarjetas = await _context.Tarjetas.Where(t => t.IdUsuario == usuario.Id).ToListAsync();
                if (!tarjetas.Any())
                {
                    return NotFound("El usuario no tiene tarjetas asociadas.");
                }

                // Obtener los IDs de las tarjetas
                var tarjetaIds = tarjetas.Select(t => t.Id).ToList();

                // Obtener las recargas de las tarjetas
                var recargas = await _context.Recargas
                    .Where(r => tarjetaIds.Contains(r.IdTarjeta))
                    .Include(r => r.Tarjeta)
                    .OrderByDescending(r => r.FechaRecarga)
                    .ToListAsync();

                if (!recargas.Any())
                {
                    return NotFound("No se encontraron recargas para el usuario proporcionado.");
                }

                // Mapear las recargas a DTOs con el número de tarjeta enmascarado
                var recargasDto = recargas.Select(r => new
                {
                    r.Id,
                    r.IdTarjeta,
                    r.Monto,
                    r.FechaRecarga,
                    r.MetodoPago,
                    Tarjeta = new TarjetaDto
                    {
                        Id = r.Tarjeta.Id,
                        Saldo = r.Tarjeta.Saldo,
                        FechaCreacion = r.Tarjeta.FechaCreacion,
                        NumeroTarjeta = r.Tarjeta.NumeroTarjetaEnmascarado,
                        IdVehiculo = r.Tarjeta.IdVehiculo
                    }
                });

                return Ok(recargasDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener el historial de recargas: {ex.Message}");
            }
        }
    }
}
