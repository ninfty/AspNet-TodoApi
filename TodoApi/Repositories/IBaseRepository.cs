namespace TodoApi.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        public Task<List<T>> GetAllAsync();
        public Task<T?> GetByIdAsync(object id);
        public Task InsertAsync(T entity);
        public Task UpdateAsync(T entity);
        public Task DeleteAsync(object id);
    }
}
