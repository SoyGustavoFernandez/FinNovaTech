using FinNovaTech.Auth.Application.Interfaces;
using FinNovaTech.Auth.Domain.Entities;
using System.Security.Cryptography;

namespace FinNovaTech.Auth.Infrastructure.Services
{
    public class AuthServices : IAuthService
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public AuthServices(IRefreshTokenRepository refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<RefreshToken> GenerateRefreshToken(int userId)
        {
            var refreshToken = new RefreshToken
            {
                AuthUserId = userId,
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expiration = DateTime.UtcNow.AddDays(7)
            };

            await _refreshTokenRepository.AddAsync(refreshToken);
            return refreshToken;
        }
    }
}