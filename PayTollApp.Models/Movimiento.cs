using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PayTollApp.Models
{
    [Table("MOVIMIENTOS", Schema = "SERVICIO")]
    public class Movimiento
    {
        [Key]
        [Column("ID_MOVIMIENTO")]
        public int? Id { get; set; }

        [Column("ID_USUARIO")]
        public int IdUsuario { get; set; }

        [Column("ID_TARJETA")]
        public int IdTarjeta { get; set; }

        [Column("ID_VEHICULO")]
        public int? IdVehiculo { get; set; }

        [Column("ID_PEAJE")]
        public int? IdPeaje { get; set; }

        [Column("TIPO_MOVIMIENTO")]
        public string? TipoMovimiento { get; set; }

        [Column("MONTO")]
        public decimal Monto { get; set; }

        [Column("SALDO_ANTERIOR")]
        public decimal SaldoAnterior { get; set; }

        [Column("SALDO_NUEVO")]
        public decimal SaldoNuevo { get; set; }

        [Column("FECHA_MOVIMIENTO")]
        public DateTime FechaMovimiento { get; set; }
    }
}
