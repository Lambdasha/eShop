// ProductService.Domain/Repositories/IProductVariationRepository.cs
namespace ProductService.Domain.Repositories;

using ProductService.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public interface IProductVariationRepository : IRepository<ProductVariationValue>
{
    Task<IEnumerable<VariationValue>> GetByProductIdAsync(int productId, CancellationToken ct = default);
}