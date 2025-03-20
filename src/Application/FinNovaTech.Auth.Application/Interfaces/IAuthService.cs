using FinNovaTech.Auth.Domain.Entities;

namespace FinNovaTech.Auth.Application.Interfaces
{
    public interface IAuthService
    {
        Task<RefreshToken> GenerateRefreshToken(int userId);
    }
}