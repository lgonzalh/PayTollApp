using System.ComponentModel.DataAnnotations;

namespace RecargasService.Models
{
    public class RecargaRequest
    {
        [Required]
        public string? Cedula { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor que cero.")]
        public decimal Monto { get; set; }

        [Required]
        public string? MetodoPago { get; set; }
    }
}
