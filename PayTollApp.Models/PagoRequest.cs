using System.ComponentModel.DataAnnotations;

namespace PayTollApp.Models
{
    public class PagoRequest
    {
        [Required]
        public string ? Cedula { get; set; }

        [Required]
        public int IdPeaje { get; set; }

        [Required]
        public int IdCategoria { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "El valor debe ser mayor que cero.")]
        public decimal Valor { get; set; }
    }
}
