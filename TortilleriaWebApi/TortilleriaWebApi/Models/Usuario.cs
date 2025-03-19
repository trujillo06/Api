using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TortilleriaWebApi.Models
{
    public class Usuario
    {
        [Key]
        public int Id_Usuario { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [EmailAddress]
        public string Correo { get; set; }

        [Required]
        public string Contraseña { get; set; }

        public DateTime Fecha_Registro { get; set; } = DateTime.UtcNow;

        [ForeignKey("Rol")]
        public int Rol { get; set; }
    }
}
