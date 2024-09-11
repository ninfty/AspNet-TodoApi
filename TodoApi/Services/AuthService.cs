using TodoApi.Models;
using TodoApi.Repositories;

namespace TodoApi.Services
{
    internal sealed class AuthService(IUserRepository userRepository, TokenService tokenService): IAuthService
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
