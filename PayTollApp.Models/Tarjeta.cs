using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PayTollApp.Models
{
    [Table("TARJETAS", Schema = "SERVICIO")]
    public class Tarjeta
    {
        [Key]
        [Column("ID_TARJETA")]
        public int Id { get; set; }

        [Column("ID_USUARIO")]
        public int IdUsuario { get; set; }

        [Column("SALDO")]
        public decimal Saldo { get; set; }

        [Column("FECHA_CREACION")]
        public DateTime FechaCreacion { get; set; }

        [Required]
        [Column("TARJETA_NUMERO")]
        public string NumeroTarjeta { get; set; } = string.Empty;

        [Column("ID_VEHICULO")]
        public int IdVehiculo { get; set; }

        // Relaciones
        [ForeignKey("IdUsuario")]
        public Usuario? Usuario { get; set; }

        [ForeignKey("IdVehiculo")]
        public Vehiculo? Vehiculo { get; set; }

        // Propiedad calculada para el número de tarjeta enmascarado
        [NotMapped]
        public string NumeroTarjetaEnmascarado
        {
            get
            {
                if (string.IsNullOrEmpty(NumeroTarjeta) || NumeroTarjeta.Length < 8)
                {
                    return NumeroTarjeta;
                }

                // Mostrar los primeros 4 y los últimos 4 dígitos, enmascarar el resto
                var primerosDigitos = NumeroTarjeta.Substring(0, 4);
                var ultimosDigitos = NumeroTarjeta.Substring(NumeroTarjeta.Length - 4);
                var asteriscos = new string('*', NumeroTarjeta.Length - 8);
                return $"{primerosDigitos}{asteriscos}{ultimosDigitos}";
            }
        }
    }
}
