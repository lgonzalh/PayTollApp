using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PayTollCardApi.Models
{
    [Table("SOLICITUDES", Schema = "USUARIO")]
    public class Solicitud
    {
        [Key]
        [Column("ID_SOLICITUD")]
        public int IdSolicitud { get; set; }

        [Column("ID_USUARIO")]
        public int IdUsuario { get; set; }

        [Column("TIPO_SOLICITUD")]
        [Required]
        [MaxLength(50)]
        public string TipoSolicitud { get; set; } = string.Empty;

        [Column("DESCRIPCION")]
        public string Descripcion { get; set; } = string.Empty;

        [Column("FECHA_SOLICITUD")]
        public DateTime FechaSolicitud { get; set; } = DateTime.Now;

        [Column("ESTADO")]
        public string Estado { get; set; } = "Pendiente";

        [ForeignKey("IdUsuario")]
        public Usuario? Usuario { get; set; }
    }
}
