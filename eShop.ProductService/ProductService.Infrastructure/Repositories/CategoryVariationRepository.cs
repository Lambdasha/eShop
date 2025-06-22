using ProductService.Domain.Entities;
using ProductService.Domain.Repositories;
using ProductService.Infrastructure.Data;

namespace ProductService.Infrastructure.Repositories;

public class CategoryVariationRepository : BaseRepository<CategoryVariation>, ICategoryVariationRepository
{
    public CategoryVariationRepository(ProductDbContext ctx) : base(ctx)
    {
    }
}