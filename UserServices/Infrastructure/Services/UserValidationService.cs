using System.Text.RegularExpressions;
using UserService.Application.Interfaces;

namespace UserService.Infrastructure.Services
{
    public class UserValidationService : IUserValidation
    {
        public Task<bool> ValidateUserFormatEmailAsync(string email)
        {
            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            var isValid = Regex.IsMatch(email, emailPattern);
            return Task.FromResult(isValid);
        }
    }
}
