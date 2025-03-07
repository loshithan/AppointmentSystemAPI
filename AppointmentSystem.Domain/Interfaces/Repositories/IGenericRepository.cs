public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<(List<T>, int)> GetAllAsync(string parameters);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);
    }