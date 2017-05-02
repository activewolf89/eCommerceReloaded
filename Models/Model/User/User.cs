using System;
using System.Collections.Generic;
<<<<<<< HEAD
using System.ComponentModel.DataAnnotations.Schema;
=======
using System.ComponentModel.DataAnnotations;
>>>>>>> 634adb61d61461c3d112f0ece25dfe401adf121f

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
        public int zipcode {get;set;}
        public string imgUrl{get;set;}
        public int wishlistId{get;set;}
        public Wishlist wishlist{get;set;}
<<<<<<< HEAD
     
        public int cartId{get;set;}
        public Cart cart{get;set;}
=======
>>>>>>> 634adb61d61461c3d112f0ece25dfe401adf121f
        public List<Order> listOfOrders{get;set;}
        public DateTime created_At {get;set;}
    }
}