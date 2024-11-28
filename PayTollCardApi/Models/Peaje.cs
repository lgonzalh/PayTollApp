using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PayTollCardApi.Models
{
    [Table("PEAJES", Schema = "SERVICIO")]
    public class Peaje
    {
        [Key]
        [Column("ID_PEAJE")]
        public int IdPeaje { get; set; }

        [Column("NOMBRE")]
        public string ? Nombre { get; set; }

        [Column("CIUDAD")]
        public string ? Ciudad { get; set; }

        [Column("DEPARTAMENTO")]
        public string ? Departamento { get; set; }
    }
}
