using System.ComponentModel.DataAnnotations;

namespace TortilleriaWebApi.Models
{
    public class RegisterModel
    {
        [Required]
        public string Nombre { get; set; }

        [Required]
        [EmailAddress]
        public string Correo { get; set; }

        [Required]
        [MinLength(6)]
        public string Contraseña { get; set; }

        public int Rol { get; set; } = 1; // Rol por defecto
    }
}
