using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace PayTollCardApi.Models
{
    [Table("PAGOS", Schema = "SERVICIO")]
    public class Pago
    {
        [Key]
        [Column("ID_PAGO")]
        public int IdPago { get; set; }

        [Column("ID_USUARIO")]
        public int IdUsuario { get; set; }

        [Column("ID_TARJETA")]
        public int IdTarjeta { get; set; }

        [Column("ID_PEAJE")]
        public int IdPeaje { get; set; }

        [Column("ID_CATEGORIA")]
        public int IdCategoria { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Valor { get; set; }

        [Column("FECHA_PAGO")]
        public DateTime FechaPago { get; set; }

        // Relaciones
        [ForeignKey("IdUsuario")]
        public Usuario? Usuario { get; set; }

        [ForeignKey("IdTarjeta")]
        public Tarjeta? Tarjeta { get; set; }

        [ForeignKey("IdPeaje")]
        public Peaje? Peaje { get; set; }

        [ForeignKey("IdCategoria")]
        public CategoriaVehiculo? CategoriaVehiculo { get; set; }
    }
}
