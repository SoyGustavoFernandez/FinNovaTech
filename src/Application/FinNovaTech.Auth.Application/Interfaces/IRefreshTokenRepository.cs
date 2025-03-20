using FinNovaTech.Auth.Domain.Entities;

namespace FinNovaTech.Auth.Application.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> GetByTokenAsync(string token);
        Task AddAsync(RefreshToken refreshToken);
        Task InvalidateTokenAsync(RefreshToken refreshToken);
    }
}