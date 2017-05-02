using System.Collections.Generic;

namespace eCommerceReloaded.Models 
{
    public class Event 
    {
        public Event()
        {
        listOfProductEvent = new List<ProductEvent>();
        }
        public int eventId{get;set;}
        public string name{get;set;}
        public int month{get;set;}
        public List<ProductEvent> listOfProductEvent {get;set;}
    }
}