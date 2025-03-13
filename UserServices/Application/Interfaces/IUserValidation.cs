namespace UserService.Application.Interfaces
{
    public interface IUserValidation
    {
        Task<bool> ValidateUserFormatEmailAsync(string email);
    }
}
