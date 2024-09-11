using TodoApi.Models;

namespace TodoApi.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        public Task<User?> GetByEmail(string email);
    }
}
