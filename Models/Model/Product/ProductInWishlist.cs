using System;
using System.ComponentModel.DataAnnotations;

namespace eCommerceReloaded.Models 
{
    public class ProductInWishlist:BaseEntity 
    {
        [Key]
        public int piwId{get;set;}
        public int productId{get;set;}
        public Product product{get;set;}
        public int wishlistId{get;set;}
        public Wishlist wishlist{get;set;}
        public DateTime created_At{get;set;}


    }
}