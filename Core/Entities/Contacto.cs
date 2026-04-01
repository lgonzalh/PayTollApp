using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PayTollCardApi.Core.Entities
{
    [Table("contactos", Schema = "usuario")]
    public class Contacto
    {
        [Key]
        [Column("id_contacto")]
        public int IdContacto { get; set; }

        [Column("id_usuario")]
        public int IdUsuario { get; set; }

        [Column("nombre")]
        public string? Nombre { get; set; }

        [Column("correo")]
        public string? Correo { get; set; }

        [Column("mensaje")]
        public string? Mensaje { get; set; }

        [Column("fecha_contacto")]
        public DateTime FechaContacto { get; set; }

        // Relación con el Usuario
        public Usuario? Usuario { get; set; }
    }
}

