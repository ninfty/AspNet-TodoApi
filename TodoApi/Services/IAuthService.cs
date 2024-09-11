namespace TodoApi.Services
{
    public interface IAuthService
    {
        public Task<string> LoginUser(string email, string password);
    }
}
