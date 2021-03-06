using System;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;

namespace eCommerceReloaded.Models 
{
    public abstract class BaseEntity{}
    public class User:BaseEntity
    {
        public User()
        {
            listOfOrders = new  List<Order>();
        }
        [Key]
        public int userId {get;set;}
        public string firstName {get;set;}
        public string lastName{get;set;}
        public string email{get;set;}
        public string password {get;set;}
        public string shipToAddress {get;set;}
        public string city {get;set;}
        public string state {get;set;}
        public string zipcode {get;set;}
        public string imgUrl{get;set;}
        public int wishlistId{get;set;}
        public Wishlist wishlist{get;set;}
        public List<Order> listOfOrders{get;set;}
        public DateTime created_At {get;set;}
    }
}