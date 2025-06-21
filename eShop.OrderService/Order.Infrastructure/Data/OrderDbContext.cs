using Microsoft.EntityFrameworkCore;
using Order.Domain.Entities;

namespace Order.Infrastructure.Data
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {
        }
        
        // ─── DbSet Properties ───
        
        public DbSet<Domain.Entities.Order>            Orders            { get; set; }
        public DbSet<OrderDetail>      OrderDetails      { get; set; }

        public DbSet<Customer>         Customers         { get; set; }
        public DbSet<UserAddress>      UserAddresses     { get; set; }
        public DbSet<Address>          Addresses         { get; set; }

        public DbSet<ShoppingCart>     ShoppingCarts     { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

        public DbSet<PaymentType>      PaymentTypes      { get; set; }
        public DbSet<PaymentMethod>    PaymentMethods    { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            base.OnModelCreating(mb);

            // ─── Orders ───
            mb.Entity<Domain.Entities.Order>()
              .HasMany(o => o.Details)
              .WithOne()                  // no navigation property back to Order
              .HasForeignKey(d => d.OrderId);

            // ─── Customers & Addresses ───
            mb.Entity<Customer>()
              .HasMany(c => c.UserAddresses)
              .WithOne(ua => ua.Customer)
              .HasForeignKey(ua => ua.CustomerId);

            mb.Entity<Address>()
              .HasMany(a => a.UserAddresses)
              .WithOne(ua => ua.Address)
              .HasForeignKey(ua => ua.AddressId);

            // ─── Shopping Cart ───
            mb.Entity<ShoppingCart>()
              .HasMany(c => c.Items)
              .WithOne(i => i.Cart)
              .HasForeignKey(i => i.CartId);

            // ─── Payments ───
            mb.Entity<PaymentType>()
              .HasMany(pt => pt.PaymentMethods)
              .WithOne(pm => pm.Type)
              .HasForeignKey(pm => pm.PaymentTypeId);
            
            // ShoppingCart → Customer
            mb.Entity<ShoppingCart>()
              .HasOne(sc => sc.Customer)
              .WithMany(c => c.ShoppingCarts)
              .HasForeignKey(sc => sc.CustomerId)
              .IsRequired()
              .OnDelete(DeleteBehavior.Cascade);

            // ShoppingCart → Items
            mb.Entity<ShoppingCart>()
              .HasMany(sc => sc.Items)
              .WithOne(i => i.Cart)
              .HasForeignKey(i => i.CartId);
        }
    }
}
