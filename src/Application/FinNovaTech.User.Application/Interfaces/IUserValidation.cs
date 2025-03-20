namespace FinNovaTech.User.Application.Interfaces
{
    public interface IUserValidation
    {
        Task<bool> ValidateUserFormatEmailAsync(string email);
    }
}