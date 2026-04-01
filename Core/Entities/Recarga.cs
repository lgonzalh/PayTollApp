using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PayTollCardApi.Core.Entities
{
    [Table("recargas", Schema = "servicio")]
    public class Recarga
    {
        [Key]
        [Column("id_recarga")]
        public int Id { get; set; }

        [Column("id_tarjeta")]
        public int IdTarjeta { get; set; }

        [Column("monto", TypeName = "numeric(18,2)")]
        public decimal Monto { get; set; }

        [Column("fecha_recarga")]
        public DateTime FechaRecarga { get; set; }

        [Column("metodo_pago")]
        public string? MetodoPago { get; set; }

        [ForeignKey("IdTarjeta")]
        public Tarjeta? Tarjeta { get; set; }
    }
}

