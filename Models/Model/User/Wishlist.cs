using System.Collections.Generic;

namespace eCommerceReloaded.Models 
{
    public class Wishlist:BaseEntity 
    {
        public Wishlist()
        {
            listOfProductInWishlist = new List<ProductInWishlist>();
        }
        public int wishlistId{get;set;}
        public int userId{get;set;}
        public User user {get;set;}
        public List<ProductInWishlist> listOfProductInWishlist{get;set;}
    }
}