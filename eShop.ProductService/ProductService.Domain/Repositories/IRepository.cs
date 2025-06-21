// ProductService.Domain/Repositories/IRepository.cs
namespace ProductService.Domain.Repositories;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

public interface IRepository<T> where T : class
{
    IQueryable<T>           Query();
    Task<IEnumerable<T>>    GetAllAsync(CancellationToken ct = default);
    Task<T?>                GetByIdAsync(int id, CancellationToken ct = default);
    Task<T>                 AddAsync(T entity, CancellationToken ct = default);
    Task<T>                 UpdateAsync(T entity, CancellationToken ct = default);
    Task<bool>              DeleteAsync(int id, CancellationToken ct = default);
}