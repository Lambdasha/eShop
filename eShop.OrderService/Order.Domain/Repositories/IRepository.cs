namespace Order.Domain.Repositories;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync(CancellationToken ct = default);
    Task<T?>             GetByIdAsync(int id, CancellationToken ct = default);
    Task<T>              AddAsync(T entity, CancellationToken ct = default);
    Task                 UpdateAsync(T entity, CancellationToken ct = default);
    Task                 DeleteAsync(int id, CancellationToken ct = default);
}