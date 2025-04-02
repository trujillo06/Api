using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TortilleriaMovilApi.Models
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_usuario { get; set; }

        [Required, MaxLength(100)]
        public string nombre { get; set; }

        [Required, EmailAddress]
        public string correo { get; set; }

        [Required]
        public string password { get; set; }

        [Required]
        public int rol { get; set; }
    }
}
