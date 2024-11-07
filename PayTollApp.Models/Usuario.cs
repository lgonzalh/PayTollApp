using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PayTollApp.Models
{
    [Table("USUARIOS", Schema = "USUARIO")]
    public class Usuario
    {
        [Key]
        [Column("ID_USUARIO")]
        public int Id { get; set; }

        [Column("CEDULA")]
        public string? Cedula { get; set; }

        [Column("NOMBRE")]
        public string? Nombre { get; set; }

        [Column("CORREO_ELECTRONICO")]
        public string? CorreoElectronico { get; set; }

        [Column("CONTRASENA")]
        [JsonIgnore]
        public string? Contrasena { get; set; }

        [Column("FECHA_CREACION")]
        public DateTime FechaCreacion { get; set; }
    }
}
