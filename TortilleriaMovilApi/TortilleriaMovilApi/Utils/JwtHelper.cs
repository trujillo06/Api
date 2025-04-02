using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TortilleriaMovilApi.Models;

namespace TortilleriaMovilApi.Utils
{
    public class JwtHelper
    {
        // Clave con al menos 16 caracteres (128 bits) para satisfacer el requisito de HMAC-SHA256
        private const string SecretKey = "Esta_Clave_Secreta_Es_Suficientemente_Larga_Para_HMAC_SHA256";

        public static string GenerateToken(Usuario user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.id_usuario.ToString()),
                    new Claim(ClaimTypes.Email, user.correo),
                    new Claim(ClaimTypes.Role, user.rol.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}