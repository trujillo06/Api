using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TortilleriaWebApi.Models;
using TortilleriaWebApi.Services;

namespace TortilleriaWebApi.Controllers
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
        public async Task<IActionResult> Register(RegisterModel model)
        {
            var result = await _authService.Register(model);
            return Ok(new { message = result });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var token = await _authService.Login(model);
            return token == "Credenciales inválidas."
                ? Unauthorized(new { message = token })
                : Ok(new { token });
        }
    }
}
