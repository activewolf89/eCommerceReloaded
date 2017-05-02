using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerceReloaded.Models 
{
    public class Cart:BaseEntity 
    {
        public Cart()
        {
            listOfProductInCart = new List<ProductInCart>();
        }
        public int cartId{get;set;}
      
        public int userId{get;set;}
        public User users{get;set;}
        public List<ProductInCart> listOfProductInCart{get;set;}
    }
}