namespace SolicitudesService.Models
{
    public class SolicitudRequest
    {
        public string? Cedula { get; set; }
        public string? TipoSolicitud { get; set; }
        public string? Descripcion { get; set; }
    }
}
