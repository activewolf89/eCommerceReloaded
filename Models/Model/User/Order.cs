using System;
using System.Collections.Generic;

namespace eCommerceReloaded.Models 
{
    public class Order 
    {
        public Order()
        {
            listOfProductInOrder = new List<ProductInOrder>();
        }
        public int orderId{get;set;}
        public int UserId{get;set;}
        public User user {get;set;}
        public DateTime created_At{get;set;}
        public List<ProductInOrder> listOfProductInOrder{get;set;}
    }
}