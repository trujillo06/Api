using System.ComponentModel.DataAnnotations;

namespace TortilleriaWebApi.Models
{
    public class RegisterModel
    {
        [Required]
        public string nombre { get; set; }

        [Required]
        [EmailAddress]
        public string correo { get; set; }

        [Required]
        [MinLength(6)]
        public string password { get; set; }

        public int rol { get; set; } = 1; // Rol por defecto
    }
}
