using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using TortilleriaMovilApi.Data;
using TortilleriaMovilApi.Dtos;
using TortilleriaMovilApi.Models;
using TortilleriaMovilApi.Utils;

namespace TortilleriaMovilApi.Services
{
    public class AuthService
    {
        private readonly ApplicationDbContext _context;

        public AuthService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> RegisterAsync(UserRegisterDto request)
        {
            if (await _context.Usuarios.AnyAsync(u => u.Correo == request.Correo))
                return false;

            var user = new Usuario
            {
                Nombre = request.Nombre,
                Correo = request.Correo,
                Contraseña = BCrypt.Net.BCrypt.HashPassword(request.Contraseña),
                Rol = request.Rol
            };

            _context.Usuarios.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<string> LoginAsync(UserLoginDto request)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == request.Correo);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Contraseña, user.Contraseña))
                return null;

            return JwtHelper.GenerateToken(user);
        }
    }
}
