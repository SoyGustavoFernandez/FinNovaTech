using AuthService.Domain.Entities;

namespace AuthService.Application.Interfaces
{
    public interface IAuthService
    {
        Task<RefreshToken> GenerateRefreshToken(int userId);
    }
}