namespace eCommerceReloaded.Models 
{
    public class ProductEvent 
    {
        public int productEventId {get;set;}
        public int eventId{get;set;}
        public Event anEvent{get;set;}
        public int productId{get;set;}
        public Product product{get;set;}
    
    }
}