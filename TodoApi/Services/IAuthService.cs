using TodoApi.Models;

namespace TodoApi.Services
{
    public interface IAuthService
    {
        public Task<string> LoginUser(string email, string password);
        public Task RegisterUser(User user);
    }
}
