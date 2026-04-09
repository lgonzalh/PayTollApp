using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using PayTollCardApi.Core.Entities;
using PayTollCardApi.Infrastructure.Persistence;
using PayTollCardApi.Web.Models;

namespace PayTollCardApi.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecargasController : ControllerBase
    {
        private readonly TarjetasDbContext _context;
        private readonly PayTollCardApi.Core.Services.TarjetaService _tarjetaService;

        public RecargasController(TarjetasDbContext context, PayTollCardApi.Core.Services.TarjetaService tarjetaService)
        {
            _context = context;
            _tarjetaService = tarjetaService;
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
                    // Si el usuario no tiene tarjeta, verificar si tiene un vehículo registrado
                    var vehiculoUsuario = await _context.Vehiculos.FirstOrDefaultAsync(v => v.IdUsuario == usuario.Id);
                    if (vehiculoUsuario != null)
                    {
                        // Crear tarjeta automáticamente para el primer vehículo encontrado
                        await _tarjetaService.CrearTarjetaParaVehiculoAsync(usuario.Id, vehiculoUsuario.Id);
                        tarjetas = await _context.Tarjetas.Where(t => t.IdUsuario == usuario.Id).ToListAsync();
                    }
                    else
                    {
                        return NotFound("El usuario no tiene vehículos registrados. Registre un vehículo para crear su tarjeta automáticamente.");
                    }
                }

                // Por simplicidad, asumamos que el usuario tiene una sola tarjeta
                var tarjeta = tarjetas.FirstOrDefault();
                if (tarjeta == null)
                {
                    return NotFound("No se encontró una tarjeta válida para el usuario.");
                }

                // Validar el monto de la recarga
                if (recargaRequest.Monto <= 0)
                {
                    return BadRequest("El monto de la recarga debe ser mayor que cero.");
                }

                var vehiculo = await _context.Vehiculos.FirstOrDefaultAsync(v => v.Id == tarjeta.IdVehiculo);
                if (vehiculo == null)
                {
                    return NotFound("El vehículo asociado a la tarjeta no existe.");
                }

                // Obtener el precio de la categoría del vehículo
                var categoriaVehiculo = await _context.CategoriasVehiculos.FirstOrDefaultAsync(c => c.IdCategoria == vehiculo.CategoriaVehiculo);
                if (categoriaVehiculo == null)
                {
                    return NotFound("La categoría del vehículo no existe.");
                }

                // Validar que el monto de la recarga sea un múltiplo del precio de la categoría
                if (recargaRequest.Monto % categoriaVehiculo.Precio != 0)
                {
                    return BadRequest($"El monto de la recarga debe ser un múltiplo de {categoriaVehiculo.Precio}.");
                }

                // Actualizar el saldo de la tarjeta
                tarjeta.Saldo += recargaRequest.Monto;

                // Crear la recarga
                var recarga = new Recarga
                {
                    IdTarjeta = tarjeta.Id,
                    Monto = recargaRequest.Monto,
                    FechaRecarga = DateTime.UtcNow,
                    MetodoPago = recargaRequest.MetodoPago
                };

                // Registrar la recarga
                _context.Recargas.Add(recarga);

                // Registrar el movimiento
                var movimiento = new Movimiento
                {
                    IdUsuario = usuario.Id,
                    IdTarjeta = tarjeta.Id,
                    Monto = recargaRequest.Monto,
                    SaldoAnterior = tarjeta.Saldo - recargaRequest.Monto,
                    SaldoNuevo = tarjeta.Saldo,
                    TipoMovimiento = "Recarga Saldo",
                    FechaMovimiento = DateTime.UtcNow,
                    IdVehiculo = tarjeta.IdVehiculo // Asegúrate de incluir el ID_VEHICULO si es necesario
                };

                _context.Movimientos.Add(movimiento);

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
                var errorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage += $" Excepción interna: {ex.InnerException.Message}";
                }

                return StatusCode(500, $"Error al realizar la recarga: {errorMessage}");
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
                var recargasDto = recargas
                    .Where(r => r.Tarjeta != null)
                    .Select(r =>
                    {
                        var tarjetaRecarga = r.Tarjeta!;
                        return new
                        {
                            r.Id,
                            r.IdTarjeta,
                            r.Monto,
                            r.FechaRecarga,
                            r.MetodoPago,
                            Tarjeta = new TarjetaDto
                            {
                                Id = tarjetaRecarga.Id,
                                Saldo = tarjetaRecarga.Saldo,
                                FechaCreacion = tarjetaRecarga.FechaCreacion,
                                NumeroTarjeta = tarjetaRecarga.NumeroTarjetaEnmascarado,
                                IdVehiculo = tarjetaRecarga.IdVehiculo
                            }
                        };
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

