using System;
using System.ComponentModel.DataAnnotations;

namespace eCommerceReloaded.Models 
{
    public class ProductInCart:BaseEntity 
    {
        [Key]
        public int picId{get;set;}
        public int productId{get;set;}
        public Product product{get;set;}
        public int cartId{get;set;}
        public Cart cart{get;set;}
        public int quantity{get;set;}
        public DateTime created_At{get;set;}


    }
}