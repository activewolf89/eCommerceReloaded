using Microsoft.EntityFrameworkCore;

namespace eCommerceReloaded.Models
{
    public class eCommerceReloadedContext : DbContext 
    {
        public eCommerceReloadedContext(DbContextOptions<eCommerceReloadedContext> options): base(options){}
        public DbSet<User> users{get;set;}
        public DbSet<Event> events{get;set;}
        public DbSet<Product> products{get;set;}
        public DbSet<Cart> carts{get;set;}
        public DbSet<Category> categories{get;set;}
        public DbSet<ProductInCart> productInCarts{get;set;}
        public DbSet<ProductInWishlist> productInWishlists{get;set;}
        public DbSet<ProductInOrder> productinOrders{get;set;}
        public DbSet<Order> orders{get;set;}
        public DbSet<Wishlist> wishlists{get;set;}
        public DbSet<ProductEvent> productEvents{get;set;}

    }
}