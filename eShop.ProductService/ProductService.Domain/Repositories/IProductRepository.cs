// ProductService.Domain/Repositories/IProductRepository.cs
namespace ProductService.Domain.Repositories;

using ProductService.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public interface IProductRepository : IRepository<Product>
{
    Task<bool> InactivateAsync(int id, CancellationToken ct = default);
}