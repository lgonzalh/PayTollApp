﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PayTollApp.Models
{
    [Table("RECARGAS", Schema = "SERVICIO")]
    public class Recarga
    {
        [Key]
        [Column("ID_RECARGA")]
        public int Id { get; set; }

        [Column("ID_TARJETA")]
        public int IdTarjeta { get; set; }

        [Column("MONTO")]
        public decimal Monto { get; set; }

        [Column("FECHA_RECARGA")]
        public DateTime FechaRecarga { get; set; }

        [Column("METODO_PAGO")]
        public string? MetodoPago { get; set; }

        [ForeignKey("IdTarjeta")]
        public Tarjeta? Tarjeta { get; set; }
    }
}
