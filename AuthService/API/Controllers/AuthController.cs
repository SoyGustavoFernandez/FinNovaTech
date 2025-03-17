using AuthService.Application.DTOs;
using AuthService.Application.Interfaces;
using AuthService.Domain.Entities;
using AuthService.Infraestructure.Data;
using AuthService.Infraestructure.Repositories;
using AuthService.Infraestructure.Services;
using AuthService.Shared;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly ILoginRepository _loginRepository;
        private readonly IAuthService _authService;
        private readonly JwtService _jwtService;
        private readonly Argon2Hasher _argon2Hasher;

        public AuthController(IRefreshTokenRepository refreshTokenRepository, ILoginRepository loginRepository, IAuthService authService, JwtService jwtService, Argon2Hasher argon2Hasher)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _authService = authService;
            _loginRepository = loginRepository;
            _jwtService = jwtService;
            _argon2Hasher = argon2Hasher;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            var existingUser = await _loginRepository.GetUserByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return BadRequest(new { message = "El correo ya está registrado" });
            }

            string salt = Guid.NewGuid().ToString();
            string hashedPassword = _argon2Hasher.HashPassword(request.Password, salt);

            var user = new User
            {
                Email = request.Email,
                PasswordHash = hashedPassword,
                Salt = salt
            };

            await _loginRepository.AddUserAsync(user);
            return Ok(new { message = "Usuario registrado correctamente" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var user = await _loginRepository.GetUserByEmailAsync(request.Email);
            if (user == null || !_argon2Hasher.VerifyPassword(request.Password, user.Salt, user.PasswordHash))
            {
                return Unauthorized(new { message = "Credenciales incorrectas" });
            }

            var token = _jwtService.GenerateToken(user);
            return Ok(new { token });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequest request)
        {
            var refreshToken = await _refreshTokenRepository.GetByTokenAsync(request.RefreshToken);

            if (refreshToken == null || refreshToken.IsRevoked || refreshToken.Expiration < DateTime.UtcNow)
            {
                return Unauthorized(new { message = "Token inválido" });
            }

            var newAccessToken = _jwtService.GenerateToken(refreshToken.AuthUser);
            var newRefreshToken = await _authService.GenerateRefreshToken(refreshToken.Id);

            await _refreshTokenRepository.InvalidateTokenAsync(refreshToken);

            return Ok(new
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken.Token
            });
        }
    }
}