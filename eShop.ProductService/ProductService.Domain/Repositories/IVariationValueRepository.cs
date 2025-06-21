// ProductService.Domain/Repositories/IVariationValueRepository.cs
namespace ProductService.Domain.Repositories;

using ProductService.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public interface IVariationValueRepository : IRepository<VariationValue>
{
    Task<IEnumerable<VariationValue>> GetByVariationIdAsync(int variationId, CancellationToken ct = default);
}