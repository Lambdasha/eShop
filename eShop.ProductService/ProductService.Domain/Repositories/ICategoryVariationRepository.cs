// ProductService.Domain/Repositories/ICategoryVariationRepository.cs
namespace ProductService.Domain.Repositories;

using ProductService.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public interface ICategoryVariationRepository : IRepository<CategoryVariation>
{
    Task<IEnumerable<CategoryVariation>> GetByCategoryIdAsync(int categoryId, CancellationToken ct = default);
}