namespace Backend.Infrastructure.Interfaces;

/// <summary>
/// Generic repository interface for data access abstraction
/// TODO(candidate): Implement concrete repository in Infrastructure layer
/// </summary>
/// <typeparam name="T">Entity type</typeparam>
public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> AddAsync(T entity);
}
