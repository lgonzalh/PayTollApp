using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PayTollCardApi.Core.Entities
{
    [Table("vehiculos", Schema = "servicio")]
    public class Vehiculo
    {
        [Key]
        [Column("id_vehiculo")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("id_usuario")]
        public int IdUsuario { get; set; }

        [Column("placa")]
        public string? Placa { get; set; }

        [Column("categoria_vehiculo")]
        public int CategoriaVehiculo { get; set; }
    }
}

