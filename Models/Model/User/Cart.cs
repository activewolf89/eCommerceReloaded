using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerceReloaded.Models 
{
    public class Cart:BaseEntity 
    {
        public Cart()
        {
            listOfProductInCart = new List<ProductInCart>();
        }

        // public int userId{get;set;}
        [Key]
        [ForeignKey("User")]
        public int cartId{get;set;}

        public virtual User user {get;set;}
        public List<ProductInCart> listOfProductInCart{get;set;}
    }

    public class CartItem:BaseEntity
    {
        public Product product {get;set;}
        public int quantity {get;set;}
    }
    public class CartContent:BaseEntity
    {
        public List<CartItem> items {get;set;}
        public int total {get;set;}
        public int quantity {get;set;}
        public CartContent()
        {
            items =new List<CartItem>();           
        }
    }
}