using TodoApi.Models;
using TodoApi.Repositories;
using TodoApi.Utils;

namespace TodoApi.Services
{
    internal sealed class AuthService(IUserRepository userRepository, TokenProvider tokenProvider): IAuthService
    {
        public async Task<string> LoginUser(string email, string password)
        {
            User? user = await userRepository.GetByEmail(email);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            //check password

            //string token = tokenService.Create(user);
            return "testtoken";
        }
    }
}
