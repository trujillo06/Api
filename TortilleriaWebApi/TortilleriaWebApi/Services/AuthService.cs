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
            if (await _context.Usuarios.AnyAsync(u => u.correo == model.correo))
                return "El correo ya está registrado.";

            var usuario = new Usuario
            {
                nombre = model.nombre,
                correo = model.correo,
                password = HashPassword(model.password),
                rol = model.rol
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return "Usuario registrado exitosamente.";
        }

        public async Task<string> Login(LoginModel model)
        {
            Console.WriteLine($"Intentando iniciar sesión con: {model.correo}");

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.correo == model.correo);

            if (usuario == null)
            {
                Console.WriteLine("Usuario no encontrado en la base de datos.");
                return "Credenciales inválidas.";
            }

            Console.WriteLine($"Usuario encontrado: {usuario.correo} - {usuario.password}");

            if (!VerifyPassword(model.password, usuario.password))
            {
                Console.WriteLine("Contraseña incorrecta.");
                return "Credenciales inválidas.";
            }

            Console.WriteLine("Inicio de sesión exitoso.");
            return _jwtHelper.GenerateToken(usuario.correo, usuario.id_usuario, usuario.rol);
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
