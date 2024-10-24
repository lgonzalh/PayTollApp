using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PayTollApp.Models
{
    [Table("VEHICULOS", Schema = "SERVICIO")]
    public class Vehiculo
    {
        [Key]
        [Column("ID_VEHICULO")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("ID_USUARIO")]
        public int IdUsuario { get; set; }

        [Column("PLACA")]
        public string? Placa { get; set; }

        [Column("CATEGORIA_VEHICULO")]
        public int CategoriaVehiculo { get; set; }
    }
}
