// ProductService.Domain/Repositories/ICategoryRepository.cs
namespace ProductService.Domain.Repositories;

using ProductService.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public interface ICategoryRepository : IRepository<ProductCategory>
{
    Task<IEnumerable<ProductCategory>> GetByParentIdAsync(int parentId, CancellationToken ct = default);
}