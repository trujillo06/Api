using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TortilleriaSucursales.Models
{
    [Table("Sucursal")]
    public class Sucursal
    {
        [Key]
        public int Id_Sucursal { get; set; }

        [Required, MaxLength(50)]
        public string Nombre { get; set; } = string.Empty;

        [Required, MaxLength(200)]
        public string Direccion { get; set; } = string.Empty;

        [Required, MaxLength(10)]
        public string Telefono_Contacto { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Nombre_Encargado { get; set; } = string.Empty;
    }
}
