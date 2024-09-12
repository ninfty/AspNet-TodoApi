using TodoApi.Models;
using TodoApi.Repositories;
using TodoApi.Utils;

namespace TodoApi.Services
{
    internal sealed class AuthService(IUserRepository userRepository, TokenProvider tokenProvider, PasswordHasher passwordHasher): IAuthService
    {
        public async Task<string> LoginUser(string email, string password)
        {
            User? user = await userRepository.GetByEmail(email);

            if (user == null)
            {
                throw new Exception("User does not exist");
            }

            bool verified = passwordHasher.Verify(password, user.Password);

            if (!verified)
            {
                throw new Exception("Invalid credentials");
            }

            return tokenProvider.Create(user);
        }

        public async Task RegisterUser(User user)
        {
            user.Password = passwordHasher.Hash(user.Password);

            await userRepository.InsertAsync(user);
        }
    }
}
