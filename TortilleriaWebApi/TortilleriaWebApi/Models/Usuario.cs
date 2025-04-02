using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TortilleriaWebApi.Models
{
    public class Usuario
    {
        [Key]
        public int id_usuario { get; set; }

        [Required]
        [StringLength(100)]
        public string nombre { get; set; }

        [Required]
        [EmailAddress]
        public string correo { get; set; }

        [Required]
        public string password { get; set; }

        public DateTime fecha_registro { get; set; } = DateTime.UtcNow;

        [ForeignKey("Rol")]
        public int rol { get; set; }
    }
}
