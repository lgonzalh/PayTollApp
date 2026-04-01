using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PayTollCardApi.Core.Entities
{
    [Table("solicitudes", Schema = "usuario")]
    public class Solicitud
    {
        [Key]
        [Column("id_solicitud")]
        public int IdSolicitud { get; set; }

        [Column("id_usuario")]
        public int IdUsuario { get; set; }

        [Column("tipo_solicitud")]
        [Required]
        [MaxLength(50)]
        public string TipoSolicitud { get; set; } = string.Empty;

        [Column("descripcion")]
        public string Descripcion { get; set; } = string.Empty;

        [Column("fecha_solicitud")]
        public DateTime FechaSolicitud { get; set; } = DateTime.UtcNow;

        [Column("estado")]
        public string Estado { get; set; } = "Pendiente";

        [ForeignKey("IdUsuario")]
        public Usuario? Usuario { get; set; }
    }
}

