// ProductService.Infrastructure/Repositories/ProductRepository.cs
namespace ProductService.Infrastructure.Repositories;

using ProductService.Domain.Entities;
using ProductService.Domain.Repositories;
using ProductService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(ProductDbContext ctx) : base(ctx)
    {
    }

    public async Task<bool> InactivateAsync(int id, CancellationToken ct = default)
    {
        // use Set<Product>() since this repo is for Product
        var product = await _ctx.Set<Product>().FindAsync(new object[]{ id }, ct);
        if (product == null) return false;

        product.Qty = 0;    // “inactivate” by zeroing stock
        await _ctx.SaveChangesAsync(ct);
        return true;
    }

}