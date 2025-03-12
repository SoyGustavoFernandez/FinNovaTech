namespace UserService.Application.Interfaces
{
    public interface IUserValidation
    {
        Task<bool> ValidateUserEmailAsync(string email);
    }
}
