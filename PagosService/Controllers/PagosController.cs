using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayTollApp.DataAccess;
using PayTollApp.Models;

namespace PagosService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagosController : ControllerBase
    {
        private readonly TarjetasDbContext _context;

        public PagosController(TarjetasDbContext context)
        {
            _context = context;
        }

        // Endpoint para realizar un pago usando la cédula
        [HttpPost("pagar")]
        public async Task<IActionResult> Pagar([FromBody] PagoRequest pagoRequest)
        {
            try
            {
                // Validar que el usuario exista usando la CEDULA
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Cedula == pagoRequest.Cedula);
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

                // Asumimos que el usuario tiene una sola tarjeta
                var tarjeta = tarjetas.FirstOrDefault();

                // Validar que el saldo sea suficiente
                if (tarjeta.Saldo < pagoRequest.Valor)
                {
                    return BadRequest("Saldo insuficiente para realizar el pago.");
                }

                // Obtener el peaje
                var peaje = await _context.Peajes.FirstOrDefaultAsync(p => p.IdPeaje == pagoRequest.IdPeaje);
                if (peaje == null)
                {
                    return NotFound("El peaje no existe.");
                }

                // Obtener la categoría del vehículo
                var categoriaVehiculo = await _context.CategoriasVehiculos.FirstOrDefaultAsync(c => c.IdCategoria == pagoRequest.IdCategoria);
                if (categoriaVehiculo == null)
                {
                    return NotFound("La categoría del vehículo no existe.");
                }

                // Actualizar el saldo de la tarjeta
                tarjeta.Saldo -= pagoRequest.Valor;

                // Registrar el pago
                var pago = new Pago
                {
                    IdUsuario = usuario.Id,
                    IdTarjeta = tarjeta.Id,
                    IdPeaje = peaje.IdPeaje,
                    IdCategoria = categoriaVehiculo.IdCategoria,
                    Valor = pagoRequest.Valor,
                    FechaPago = DateTime.Now
                };

                _context.Pagos.Add(pago);

                // Guardar cambios en la base de datos
                await _context.SaveChangesAsync();

                // Crear el objeto de respuesta con la información enmascarada
                var pagoDto = new
                {
                    Mensaje = "Pago realizado exitosamente.",
                    Pago = new
                    {
                        pago.IdPago,
                        pago.Valor,
                        pago.FechaPago,
                        IdPeaje = pago.IdPeaje // Agregar el ID_PEAJE a la respuesta
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

                return Ok(pagoDto);
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage += $" Excepción interna: {ex.InnerException.Message}";
                }
                return StatusCode(500, $"Error al realizar el pago: {errorMessage}");
            }
        }

        // Endpoint para obtener el historial de pagos usando la cédula
        [HttpGet("historial/{cedula}")]
        public async Task<IActionResult> Historial(string cedula)
        {
            try
            {
                // Validar que el usuario exista usando la CEDULA
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Cedula == cedula);
                if (usuario == null)
                {
                    return NotFound("El usuario no existe.");
                }

                // Obtener los pagos del usuario
                var pagos = await _context.Pagos
                    .Where(p => p.IdUsuario == usuario.Id)
                    .Include(p => p.Tarjeta)
                    .OrderByDescending(p => p.FechaPago)
                    .ToListAsync();

                if (!pagos.Any())
                {
                    return NotFound("No se encontraron pagos para el usuario proporcionado.");
                }

                // Mapear los pagos a DTOs
                var pagosDto = pagos.Select(p => new
                {
                    p.IdPago,
                    p.IdTarjeta,
                    p.Valor,
                    p.FechaPago,
                    Tarjeta = new TarjetaDto
                    {
                        Id = p.Tarjeta.Id,
                        Saldo = p.Tarjeta.Saldo,
                        FechaCreacion = p.Tarjeta.FechaCreacion,
                        NumeroTarjeta = p.Tarjeta.NumeroTarjetaEnmascarado,
                        IdVehiculo = p.Tarjeta.IdVehiculo
                    }
                });

                return Ok(pagosDto);
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage += $" Excepción interna: {ex.InnerException.Message}";
                }
                return StatusCode(500, $"Error al obtener el historial de pagos: {errorMessage}");
            }
        }
    }
}
