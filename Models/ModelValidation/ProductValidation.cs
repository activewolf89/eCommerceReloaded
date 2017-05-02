using System.ComponentModel.DataAnnotations;

namespace eCommerceReloaded.Models
{
public class ProductValidation
    {
        [Required]
        public string name {get;set;}
        [Required]
        public string imageUrl{get;set;}
        [Required]
        [Range(0,100000000)]
        public int quantity{get;set;}
        [Required]
        public string category{get;set;}
        [Required]
        [Range(0,100000000)]
        public int cost{get;set;}
        [Required]
        [Range(0,100000000)]
        public int price{get;set;}

    }
}