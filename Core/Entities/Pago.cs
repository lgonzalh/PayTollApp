using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace PayTollCardApi.Core.Entities
{
    [Table("pagos", Schema = "servicio")]
    public class Pago
    {
        [Key]
        [Column("id_pago")]
        public int IdPago { get; set; }

        [Column("id_usuario")]
        public int IdUsuario { get; set; }

        [Column("id_tarjeta")]
        public int IdTarjeta { get; set; }

        [Column("id_peaje")]
        public int IdPeaje { get; set; }

        [Column("id_categoria")]
        public int IdCategoria { get; set; }

        [Column("valor", TypeName = "numeric(18,2)")]
        public decimal Valor { get; set; }

        [Column("fecha_pago")]
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

