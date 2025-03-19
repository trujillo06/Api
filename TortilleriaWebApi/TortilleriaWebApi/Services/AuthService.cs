using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using TortilleriaWebApi.Data;
using TortilleriaWebApi.Models;
using TortilleriaWebApi.Utils;

namespace TortilleriaWebApi.Services
{
    public class AuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly JwtHelper _jwtHelper;

        public AuthService(ApplicationDbContext context, JwtHelper jwtHelper)
        {
            _context = context;
            _jwtHelper = jwtHelper;
        }

        public async Task<string> Register(RegisterModel model)
        {
            if (await _context.Usuarios.AnyAsync(u => u.Correo == model.Correo))
                return "El correo ya está registrado.";

            var usuario = new Usuario
            {
                Nombre = model.Nombre,
                Correo = model.Correo,
                Contraseña = HashPassword(model.Contraseña),
                Rol = model.Rol
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return "Usuario registrado exitosamente.";
        }

        public async Task<string> Login(LoginModel model)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == model.Correo);
            if (usuario == null || !VerifyPassword(model.Contraseña, usuario.Contraseña))
                return "Credenciales inválidas.";

            return _jwtHelper.GenerateToken(usuario.Correo, usuario.Id_Usuario, usuario.Rol);
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            return Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(password)));
        }

        private bool VerifyPassword(string inputPassword, string hashedPassword)
        {
            return HashPassword(inputPassword) == hashedPassword;
        }
    }
}
