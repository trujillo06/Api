using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TortilleriaMovilApi.Dtos;
using TortilleriaMovilApi.Services;

namespace TortilleriaMovilApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto request)
        {
            var result = await _authService.RegisterAsync(request);
            return result ? Ok(new { message = "Usuario registrado exitosamente" }) : BadRequest("Error en el registro");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto request)
        {
            var token = await _authService.LoginAsync(request);
            return token != null ? Ok(new { token }) : Unauthorized("Credenciales incorrectas");
        }
    }
}
