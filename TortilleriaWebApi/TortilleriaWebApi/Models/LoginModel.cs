using System.ComponentModel.DataAnnotations;

namespace TortilleriaWebApi.Models
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string Correo { get; set; }

        [Required]
        public string Contraseña { get; set; }
    }
}
