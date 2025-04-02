using System.ComponentModel.DataAnnotations;

namespace TortilleriaWebApi.Models
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string correo { get; set; }

        [Required]
        public string password { get; set; }
    }
}
