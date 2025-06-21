// ProductService.Infrastructure/Repositories/BaseRepository.cs
namespace ProductService.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using ProductService.Domain.Repositories;
using ProductService.Infrastructure.Data;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

public class BaseRepository<T> : IRepository<T> where T : class
{
    protected readonly ProductDbContext _ctx;

    public BaseRepository(ProductDbContext ctx)
    {
        _ctx = ctx;
    }

    public IQueryable<T> Query() 
        => _ctx.Set<T>().AsNoTracking();

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken ct = default)
        => await _ctx.Set<T>().AsNoTracking().ToListAsync(ct);

    public async Task<T?> GetByIdAsync(int id, CancellationToken ct = default)
        => await _ctx.Set<T>().FindAsync(id, ct);

    public async Task<T> AddAsync(T entity, CancellationToken ct = default)
    {
        _ctx.Set<T>().Add(entity);
        await _ctx.SaveChangesAsync(ct);
        return entity;
    }
    public async Task<T> UpdateAsync(T entity, CancellationToken ct = default)
    {
        _ctx.Entry(entity).State = EntityState.Modified;
        await _ctx.SaveChangesAsync(ct);
        return entity;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
    {
        var entity = await GetByIdAsync(id, ct) ?? throw new KeyNotFoundException();
        _ctx.Set<T>().Remove(entity);
        await _ctx.SaveChangesAsync(ct);
        return true;
    }
}