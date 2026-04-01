using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PayTollCardApi.Core.Entities
{
    [Table("categorias_vehiculos", Schema = "servicio")]
    public class CategoriaVehiculo
    {
        [Key]
        [Column("id_categoria")]
        public int IdCategoria { get; set; }

        [Column("nombre_categoria")]
        public string? NombreCategoria { get; set; }

        [Column("descripcion")]
        public string? Descripcion { get; set; }

        [Column("precio", TypeName = "numeric(18,2)")]
        public decimal Precio { get; set; }
    }
}

