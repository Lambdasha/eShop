// ProductService.Domain/Repositories/IProductRepository.cs
namespace ProductService.Domain.Repositories;

using ProductService.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public interface IProductRepository : IRepository<Product>
{
    Task<(IEnumerable<Product> Items,int Total)> 
        GetPagedAsync(int page,int size, CancellationToken ct = default);

    Task<IEnumerable<Product>> 
        GetByCategoryIdAsync(int categoryId, CancellationToken ct = default);

    Task<IEnumerable<Product>> 
        GetByNameAsync(string name, CancellationToken ct = default);

    Task<bool> InactivateAsync(int id, CancellationToken ct = default);
}