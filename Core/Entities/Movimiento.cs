using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PayTollCardApi.Core.Entities
{
    [Table("movimientos", Schema = "servicio")]
    public class Movimiento
    {
        [Key]
        [Column("id_movimiento")]
        public int? Id { get; set; }

        [Column("id_usuario")]
        public int IdUsuario { get; set; }

        [Column("id_tarjeta")]
        public int IdTarjeta { get; set; }

        [Column("id_vehiculo")]
        public int? IdVehiculo { get; set; }

        [Column("id_peaje")]
        public int? IdPeaje { get; set; }

        [Column("tipomovimiento")]
        public string? TipoMovimiento { get; set; }

        [Column("monto", TypeName = "numeric(18,2)")]
        public decimal Monto { get; set; }

        [Column("saldoanterior", TypeName = "numeric(18,2)")]
        public decimal SaldoAnterior { get; set; }

        [Column("saldonuevo", TypeName = "numeric(18,2)")]
        public decimal SaldoNuevo { get; set; }

        [Column("fecha_movimiento")]
        public DateTime FechaMovimiento { get; set; }
    }
}

