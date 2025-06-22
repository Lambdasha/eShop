// ProductService.Domain/Repositories/ICategoryRepository.cs
namespace ProductService.Domain.Repositories;

using ProductService.Domain.Entities;
using System.Linq;

public interface ICategoryRepository : IRepository<ProductCategory>
{

}