using System.Collections.Generic;

namespace eCommerceReloaded.Models 
{
    public class Cart 
    {
        public Cart()
        {
            listOfProductInCart = new List<ProductInCart>();
        }
        public int cartId{get;set;}
        public int UserId{get;set;}
        public User user {get;set;}
        public List<ProductInCart> listOfProductInCart{get;set;}
    }
}