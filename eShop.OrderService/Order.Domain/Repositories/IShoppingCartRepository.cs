// Order.Domain/Repositories/IShoppingCartRepository.cs
namespace Order.Domain.Repositories;

using Order.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

public interface IShoppingCartRepository : IRepository<ShoppingCart>
{
    /// <summary>Get the cart (with items) for a customer, or null if none.</summary>
    Task<ShoppingCart?> GetByCustomerIdAsync(int customerId, CancellationToken ct = default);

    /// <summary>Delete the cart (and its items) for a customer.</summary>
    Task DeleteByCustomerIdAsync(int customerId, CancellationToken ct = default);
    Task<bool> DeleteItemByIdAsync(int itemId, CancellationToken ct = default);
}