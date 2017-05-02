using System;
using System.Collections.Generic;

namespace eCommerceReloaded.Models 
{
    public class Category:BaseEntity 
    {
        public Category()
        {
            listOfProducts = new List<Product>();
        }
        public int categoryId{get;set;}
        public int name{get;set;}
        public DateTime created_At{get;set;}
        public List<Product> listOfProducts{get;set;}

    }
}