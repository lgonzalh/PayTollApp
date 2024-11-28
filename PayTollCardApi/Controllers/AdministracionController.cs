using Microsoft.AspNetCore.Mvc;
using PayTollCardApi.Services;
using PayTollCardApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PayTollCardApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministracionController : ControllerBase
    {
        private readonly IAdministradorService _administradorService;

        public AdministracionController(IAdministradorService administradorService)
        {
            _administradorService = administradorService;
        }

        // GET: api/Administracion/usuarios
        [HttpGet("usuarios")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            var usuarios = await _administradorService.GetAllUsuariosAsync();
            return Ok(usuarios);
        }

        // GET: api/Administracion/usuarios/{cedula}
        [HttpGet("usuarios/{cedula}")]
        public async Task<ActionResult<Usuario>> GetUsuarioByCedula(string cedula)
        {
            var usuario = await _administradorService.GetUsuarioByCedulaAsync(cedula);
            if (usuario == null)
            {
                return NotFound("Usuario no encontrado.");
            }
            return Ok(usuario);
        }

        // POST: api/Administracion/usuarios
        [HttpPost("usuarios")]
        public async Task<ActionResult<Usuario>> CreateUsuario([FromBody] Usuario usuario)
        {
            var createdUsuario = await _administradorService.CreateUsuarioAsync(usuario);
            return CreatedAtAction(nameof(GetUsuarioByCedula), new { cedula = createdUsuario.Cedula }, createdUsuario);
        }

        // PUT: api/Administracion/usuarios/{cedula}
        [HttpPut("usuarios/{cedula}")]
        public async Task<IActionResult> UpdateUsuario(string cedula, [FromBody] Usuario usuario)
        {
            await _administradorService.UpdateUsuarioByCedulaAsync(cedula, usuario);
            return NoContent();
        }

        // DELETE: api/Administracion/usuarios/{cedula}
        [HttpDelete("usuarios/{cedula}")]
        public async Task<IActionResult> DeleteUsuario(string cedula)
        {
            await _administradorService.DeleteUsuarioByCedulaAsync(cedula);
            return NoContent();
        }

        // GET: api/Administracion/tarjetas/{cedula}
        [HttpGet("tarjetas/{cedula}")]
        public async Task<ActionResult<IEnumerable<Tarjeta>>> GetTarjetas(string cedula)
        {
            var tarjetas = await _administradorService.GetTarjetasByCedulaAsync(cedula);
            return Ok(tarjetas);
        }

        // POST: api/Administracion/tarjetas
        [HttpPost("tarjetas")]
        public async Task<ActionResult<Tarjeta>> CreateTarjeta([FromBody] Tarjeta tarjeta)
        {
            var createdTarjeta = await _administradorService.CreateTarjetaAsync(tarjeta);
            return CreatedAtAction(nameof(GetTarjetas), new { cedula = createdTarjeta.IdUsuario }, createdTarjeta);
        }

        // PUT: api/Administracion/tarjetas/{numeroTarjeta}
        [HttpPut("tarjetas/{numeroTarjeta}")]
        public async Task<IActionResult> UpdateTarjeta(string numeroTarjeta, [FromBody] Tarjeta tarjeta)
        {
            await _administradorService.UpdateTarjetaAsync(tarjeta);
            return NoContent();
        }

        // DELETE: api/Administracion/tarjetas/{numeroTarjeta}
        [HttpDelete("tarjetas/{numeroTarjeta}")]
        public async Task<IActionResult> DeleteTarjeta(string numeroTarjeta)
        {
            await _administradorService.DeleteTarjetaAsync(numeroTarjeta);
            return NoContent();
        }

        // GET: api/Administracion/recargas/{cedula}
        [HttpGet("recargas/{cedula}")]
        public async Task<ActionResult<IEnumerable<Recarga>>> GetRecargas(string cedula)
        {
            var recargas = await _administradorService.GetAllRecargasByCedulaAsync(cedula);
            return Ok(recargas);
        }

        // GET: api/Administracion/pagos/{cedula}
        [HttpGet("pagos/{cedula}")]
        public async Task<ActionResult<IEnumerable<Pago>>> GetPagos(string cedula)
        {
            var pagos = await _administradorService.GetAllPagosByCedulaAsync(cedula);
            return Ok(pagos);
        }

        // GET: api/Administracion/solicitudes/{cedula}
        [HttpGet("solicitudes/{cedula}")]
        public async Task<ActionResult<IEnumerable<Solicitud>>> GetSolicitudes(string cedula)
        {
            var solicitudes = await _administradorService.GetAllSolicitudesByCedulaAsync(cedula);
            return Ok(solicitudes);
        }

        // Otros endpoints...
    }
}
