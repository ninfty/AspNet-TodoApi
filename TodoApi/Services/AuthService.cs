namespace TodoApi.Services
{
    internal sealed class AuthService(TokenService tokenService)
    {
        public async Task<string> LoginUser()
        {

            //string token = tokenService.Create(user);
            return "testtoken";
        }
    }
}
