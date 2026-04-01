using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PayTollCardApi.Core.Entities
{
    [Table("usuarios", Schema = "usuario")]
    public class Usuario
    {
        [Key]
        [Column("id_usuario")]
        public int Id { get; set; }

        [Column("cedula")]
        public string? Cedula { get; set; }

        [Column("nombre")]
        public string? Nombre { get; set; }

        [Column("correo_electronico")]
        public string? CorreoElectronico { get; set; }

        [Column("contrasena")]
        public string? Contrasena { get; set; }

        [Column("fecha_creacion")]
        public DateTime FechaCreacion { get; set; }
    }
}

