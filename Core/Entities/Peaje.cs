using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PayTollCardApi.Core.Entities
{
    [Table("peajes", Schema = "servicio")]
    public class Peaje
    {
        [Key]
        [Column("id_peaje")]
        public int IdPeaje { get; set; }

        [Column("nombre")]
        public string? Nombre { get; set; }

        [Column("ciudad")]
        public string? Ciudad { get; set; }

        [Column("departamento")]
        public string? Departamento { get; set; }
    }
}

