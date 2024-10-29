using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PayTollApp.Models
{
    [Table("CONTACTOS", Schema = "USUARIO")]
    public class Contacto
    {
        [Key]
        [Column("ID_CONTACTO")]
        public int IdContacto { get; set; }

        [Column("ID_USUARIO")]
        public int IdUsuario { get; set; }

        [Column("NOMBRE")]
        public string? Nombre { get; set; }

        [Column("CORREO")]
        public string? Correo { get; set; }

        [Column("MENSAJE")]
        public string? Mensaje { get; set; }

        [Column("FECHA_CONTACTO")]
        public DateTime FechaContacto { get; set; }

        // Relación con el Usuario
        public Usuario? Usuario { get; set; }
    }
}
