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

    public async Task<(IEnumerable<Product>, int)> GetPagedAsync(
        int page, int size, CancellationToken ct = default)
    {
        var q     = Query().OrderBy(p => p.Name);
        var total = await q.CountAsync(ct);
        var items = await q.Skip((page - 1)*size).Take(size).ToListAsync(ct);
        return (items, total);
    }

    public Task<IEnumerable<Product>> GetByCategoryIdAsync(
        int categoryId, CancellationToken ct = default)
        => Query()
            .Where(p => p.CategoryId == categoryId)
            .ToListAsync(ct)
            .ContinueWith(t => (IEnumerable<Product>)t.Result, ct);

    public Task<IEnumerable<Product>> GetByNameAsync(
        string name, CancellationToken ct = default)
        => Query()
            .Where(p => p.Name.Contains(name))
            .ToListAsync(ct)
            .ContinueWith(t => (IEnumerable<Product>)t.Result, ct);

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