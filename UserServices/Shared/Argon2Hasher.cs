using Konscious.Security.Cryptography;
using System.Text;

namespace UserService.Shared
{
    public class Argon2Hasher
    {
        private readonly int _memorySize = 65536;
        private readonly int _iterations = 3;
        private readonly int _parallelism = 4;
        private readonly int _hashLength = 32;

        public string HashPassword(string password, string salt)
        {
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = Encoding.UTF8.GetBytes(salt),
                DegreeOfParallelism = _parallelism,
                MemorySize = _memorySize,
                Iterations = _iterations
            };

            byte[] hash = argon2.GetBytes(_hashLength);
            return Convert.ToBase64String(hash);
        }

        public bool VerifyPassword(string password, string salt, string hashedPassword)
        {
            string newHash = HashPassword(password, salt);
            return newHash == hashedPassword;
        }
    }
}