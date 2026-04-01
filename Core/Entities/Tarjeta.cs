using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PayTollCardApi.Core.Entities
{
    [Table("tarjetas", Schema = "servicio")]
    public class Tarjeta
    {
        [Key]
        [Column("id_tarjeta")]
        public int Id { get; set; }

        [Column("id_usuario")]
        public int IdUsuario { get; set; }

        [Column("saldo", TypeName = "numeric(18,2)")]
        public decimal Saldo { get; set; }

        [Column("fecha_creacion")]
        public DateTime FechaCreacion { get; set; }

        [Required]
        [Column("tarjeta_numero")]
        public string NumeroTarjeta { get; set; } = string.Empty;

        [Column("id_vehiculo")]
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

