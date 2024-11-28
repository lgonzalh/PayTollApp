﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PayTollCardApi.Models
{
    [Table("CATEGORIAS_VEHICULOS", Schema = "SERVICIO")]
    public class CategoriaVehiculo
    {
        [Key]
        [Column("ID_CATEGORIA")]
        public int IdCategoria { get; set; }

        [Column("NOMBRE_CATEGORIA")]
        public string ? NombreCategoria { get; set; }

        [Column("DESCRIPCION")]
        public string ? Descripcion { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Precio { get; set; }
    }
}
