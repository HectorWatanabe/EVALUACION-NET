using ExperisEvaluacionAPI.DbContexts;
using ExperisEvaluacionAPI.Models;
using ExperisEvaluacionAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ExperisEvaluacionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UsuariosContext _context;
        private readonly TokenService _tokenService;

        private static readonly Dictionary<string, string> _refreshTokens = new();

        private readonly IPasswordHasherService _passwordHasherService;

        public LoginController(UsuariosContext context, TokenService tokenService, IPasswordHasherService passwordHasherService)
        {
            _context = context;
            _tokenService = tokenService;
            _passwordHasherService = passwordHasherService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Email == login.Email);

            if (usuario == null)
            {
                return Unauthorized("Invalid credentials");
            }

            if (usuario.Bloquear)
            {
                return Unauthorized("Usuario bloqueado");
            }

            if (!_passwordHasherService.VerifyPassword(usuario.Password, login.Password))
            {
                return Unauthorized("Invalid credentials");
            }

            var claims = new List<Claim> { new Claim(ClaimTypes.Name, login.Email) };
            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            _refreshTokens[refreshToken] = login.Email;

            return Ok(new { AccessToken = accessToken, RefreshToken = refreshToken });

        }

        [HttpPost("Refresh")]
        [Authorize]
        public IActionResult Refresh(RefreshRequest request)
        {
            if (!_refreshTokens.ContainsKey(request.RefreshToken))
            {
                return Unauthorized();
            }

            var principal = _tokenService.GetPrincipalFromExpiredToken(request.AccessToken);

            var email = principal.Identity.Name;

            if (email != _refreshTokens[request.RefreshToken])
            {
                return Unauthorized();
            }

            var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            _refreshTokens.Remove(request.RefreshToken);
            _refreshTokens[newRefreshToken] = email;

            return Ok(new { AccessToken = newAccessToken, RefreshToken = newRefreshToken });
        }
    }
}
