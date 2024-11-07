using Microsoft.AspNetCore.Mvc;
using AdministradorService.Services;
using PayTollApp.Models;

namespace AdministradorService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdministracionController : ControllerBase
    {
        private readonly IAdministradorService _administradorService;

        public AdministracionController(IAdministradorService administradorService)
        {
            _administradorService = administradorService;
        }

        // Gestión de Usuarios
        [HttpGet("usuarios")]
        public async Task<IActionResult> GetUsuarios()
        {
            var usuarios = await _administradorService.GetAllUsuariosAsync();
            return Ok(usuarios);
        }

        [HttpPost("usuarios")]
        public async Task<IActionResult> CreateUsuario(Usuario usuario)
        {
            var createdUsuario = await _administradorService.CreateUsuarioAsync(usuario);
            return CreatedAtAction(nameof(GetUsuarios), new { cedula = createdUsuario.Cedula }, createdUsuario);
        }

        [HttpPut("usuarios/{cedula}")]
        public async Task<IActionResult> UpdateUsuario(string cedula, Usuario usuario)
        {
            await _administradorService.UpdateUsuarioByCedulaAsync(cedula, usuario);
            return NoContent(); // No hay que asignar el resultado de un método `void`.
        }

        [HttpDelete("usuarios/{cedula}")]
        public async Task<IActionResult> DeleteUsuario(string cedula)
        {
            await _administradorService.DeleteUsuarioByCedulaAsync(cedula);
            return NoContent(); // No hay que asignar el resultado de un método `void`.
        }

        // Gestión de Tarjetas
        [HttpGet("tarjetas/{cedula}")]
        public async Task<IActionResult> GetTarjetasByCedula(string cedula)
        {
            var tarjetas = await _administradorService.GetTarjetasByCedulaAsync(cedula);
            return Ok(tarjetas);
        }

        [HttpPost("tarjetas")]
        public async Task<IActionResult> CreateTarjeta(Tarjeta tarjeta)
        {
            var createdTarjeta = await _administradorService.CreateTarjetaAsync(tarjeta);
            return CreatedAtAction(nameof(GetTarjetasByCedula), new { cedula = tarjeta.IdUsuario }, createdTarjeta);
        }

        [HttpPut("tarjetas/{numeroTarjeta}")]
        public async Task<IActionResult> UpdateTarjeta(string numeroTarjeta, Tarjeta tarjeta)
        {
            tarjeta.NumeroTarjeta = numeroTarjeta; // Actualizar la tarjeta con el número especificado.
            await _administradorService.UpdateTarjetaAsync(tarjeta);
            return NoContent();
        }

        [HttpDelete("tarjetas/{numeroTarjeta}")]
        public async Task<IActionResult> DeleteTarjeta(string numeroTarjeta)
        {
            await _administradorService.DeleteTarjetaAsync(numeroTarjeta);
            return NoContent();
        }

        // Gestión de Recargas
        [HttpGet("recargas/{cedula}")]
        public async Task<IActionResult> GetRecargasByCedula(string cedula)
        {
            var recargas = await _administradorService.GetAllRecargasByCedulaAsync(cedula);
            return Ok(recargas);
        }

        // Gestión de Pagos
        [HttpGet("pagos/{cedula}")]
        public async Task<IActionResult> GetPagosByCedula(string cedula)
        {
            var pagos = await _administradorService.GetAllPagosByCedulaAsync(cedula);
            return Ok(pagos);
        }

        // Gestión de Solicitudes
        [HttpGet("solicitudes/{cedula}")]
        public async Task<IActionResult> GetSolicitudesByCedula(string cedula)
        {
            var solicitudes = await _administradorService.GetAllSolicitudesByCedulaAsync(cedula);
            return Ok(solicitudes);
        }

        [HttpPost("solicitudes")]
        public async Task<IActionResult> CreateSolicitud(Solicitud solicitud)
        {
            var createdSolicitud = await _administradorService.CreateSolicitudAsync(solicitud);
            return CreatedAtAction(nameof(GetSolicitudesByCedula), new { cedula = solicitud.IdUsuario }, createdSolicitud);
        }

        [HttpPut("solicitudes/{idSolicitud}")]
        public async Task<IActionResult> UpdateSolicitud(int idSolicitud, Solicitud solicitud)
        {
            solicitud.IdSolicitud = idSolicitud; // Actualizar la solicitud con el ID especificado.
            await _administradorService.UpdateSolicitudAsync(solicitud);
            return NoContent();
        }

        [HttpDelete("solicitudes/{idSolicitud}")]
        public async Task<IActionResult> DeleteSolicitud(int idSolicitud)
        {
            await _administradorService.DeleteSolicitudAsync(idSolicitud);
            return NoContent();
        }
    }
}
