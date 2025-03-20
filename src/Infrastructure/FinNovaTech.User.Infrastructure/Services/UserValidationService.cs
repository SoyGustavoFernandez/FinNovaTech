using FinNovaTech.User.Application.Interfaces;
using System.Text.RegularExpressions;

namespace FinNovaTech.User.Infrastructure.Services
{
    public class UserValidationService : IUserValidation
    {
        private static readonly TimeSpan RegexTimeout = TimeSpan.FromMilliseconds(500);

        private static readonly Regex EmailRegex = new(
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            RegexOptions.Compiled,
            RegexTimeout
        );

        public async Task<bool> ValidateUserFormatEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            return await Task.Run(() => EmailRegex.IsMatch(email));
        }
    }
}