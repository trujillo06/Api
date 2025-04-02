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
            if (await _context.Usuarios.AnyAsync(u => u.correo == request.correo))
                return false;

            var user = new Usuario
            {
                nombre = request.nombre,
                correo = request.correo,
                password = BCrypt.Net.BCrypt.HashPassword(request.password),
                rol = request.rol
            };

            _context.Usuarios.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<string> LoginAsync(UserLoginDto request)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.correo == request.correo);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.password, user.password))
                return null;

            return JwtHelper.GenerateToken(user);
        }
    }
}
