using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using PayTollCardApi.Infrastructure.Persistence;
using PayTollCardApi.Web.Models;

namespace PayTollCardApi.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExtractoController : ControllerBase
    {
        private readonly TarjetasDbContext _context;

        public ExtractoController(TarjetasDbContext context)
        {
            _context = context;
        }

        [HttpGet("{cedula}")]
        public async Task<IActionResult> ObtenerExtracto(string cedula)
        {
            try
            {
                // Obtener el usuario por cédula
                var usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.Cedula == cedula);

                if (usuario == null)
                    return NotFound("Usuario no encontrado.");

                // Obtener la tarjeta del usuario
                var tarjeta = await _context.Tarjetas
                    .FirstOrDefaultAsync(t => t.IdUsuario == usuario.Id);

                if (tarjeta == null)
                    return NotFound("No se encontró tarjeta asociada.");

                // Obtener los pagos realizados
                var pagos = await _context.Pagos
                    .Where(p => p.IdUsuario == usuario.Id)
                    .OrderBy(p => p.FechaPago)
                    .ToListAsync();

                // Obtener las recargas realizadas
                var recargas = await _context.Recargas
                    .Where(r => r.IdTarjeta == tarjeta.Id)
                    .OrderBy(r => r.FechaRecarga)
                    .ToListAsync();

                var totalRecargas = recargas.Sum(r => r.Monto);
                var totalPagos = pagos.Sum(p => p.Valor);
                var saldoInicial = tarjeta.Saldo - totalRecargas + totalPagos;
                var saldoAcumulado = saldoInicial;

                // Crear lista de movimientos consolidada
                var movimientosBase = pagos.Select(p => new
                {
                    Fecha = p.FechaPago,
                    Descripcion = "Pago Peaje",
                    Peaje = _context.Peajes.FirstOrDefault(pe => pe.IdPeaje == p.IdPeaje)?.Nombre ?? "N/A",
                    Valor = -p.Valor
                }).Concat(recargas.Select(r => new
                {
                    Fecha = r.FechaRecarga,
                    Descripcion = "Recarga Saldo",
                    Peaje = "N/A",
                    Valor = r.Monto
                }))
                .OrderBy(m => m.Fecha)
                .ToList();

                var movimientos = movimientosBase.Select(m =>
                {
                    saldoAcumulado += m.Valor;

                    return new MovimientoDto
                    {
                        Fecha = m.Fecha,
                        Descripcion = m.Descripcion,
                        Peaje = m.Peaje,
                        Valor = m.Valor,
                        Saldo = saldoAcumulado
                    };
                }).ToList();

                // Crear el extracto consolidado
                var extracto = new ExtractoDto
                {
                    NumeroTarjeta = tarjeta.NumeroTarjetaEnmascarado,
                    SaldoAnterior = saldoInicial,
                    TotalRecargas = totalRecargas,
                    TotalPagos = totalPagos,
                    SaldoActual = tarjeta.Saldo,
                    Movimientos = movimientos
                };

                return Ok(extracto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al generar el extracto: {ex.Message}");
            }
        }
    }
}

