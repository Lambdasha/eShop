using ProductService.Domain.Entities;
using ProductService.Domain.Repositories;
using ProductService.Infrastructure.Data;

namespace ProductService.Infrastructure.Repositories;

public class CategoryRepository : BaseRepository<ProductCategory>, ICategoryRepository
{
    public CategoryRepository(ProductDbContext ctx) : base(ctx)
    {
    }
}