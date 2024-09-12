using System.Security.Cryptography;

namespace TodoApi.Utils
{
    public sealed class PasswordHasher
    {
        private const int saltLength = 16;
        private const int hashLength = 32;
        private const int iterations = 10000;
        private static readonly HashAlgorithmName algorithm = HashAlgorithmName.SHA512;

        public string Hash(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(saltLength);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, algorithm, hashLength);

            return $"{Convert.ToHexString(hash)}-{Convert.ToHexString(salt)}";
        }

        public bool Verify(string password, string passwordHash)
        {
            string[] parts = passwordHash.Split('-');
            byte[] hash = Convert.FromHexString(parts[0]);
            byte[] salt = Convert.FromHexString(parts[1]);

            byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, algorithm, hashLength);

            return CryptographicOperations.FixedTimeEquals(hash, inputHash);
        }
    }
}
