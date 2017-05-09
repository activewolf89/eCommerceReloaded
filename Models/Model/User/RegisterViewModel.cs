using System.ComponentModel.DataAnnotations;

namespace eCommerceReloaded.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "First name is required.")]
        [MinLength(2,ErrorMessage = "Min length is 2.")]
        [RegularExpression(@"^[a-zA-Z]{1,40}$", ErrorMessage = "Only letter are allowed.")]       
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [MinLength(2,ErrorMessage = "Min length is 2.")]
        [RegularExpression(@"^[a-zA-Z]{1,40}$", ErrorMessage = "Only letter are allowed.")]   
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Not a valid email")]
        public string Email { get; set; }

        [Display(Name = "Ship to address")]
        [Required(ErrorMessage = "Ship address is required.")]
        public string shipToAddress {get;set;}

        [Display(Name = "City")]
        [Required(ErrorMessage = "City is required.")]
        public string city {get;set;}

        [Display(Name = "Stata")]
        [Required(ErrorMessage = "State is required.")]
        public string state {get;set;}

        [Required(ErrorMessage = "Zipcode is required.")]
        [Display(Name = "Zipcode")]
        [RegularExpression(@"^\d{5}(?:[-\s]\d{4})?$", ErrorMessage = "Input is not a valid zipcode.")]   
        public string zipcode {get;set;}

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [MinLength(8,ErrorMessage = "Min length is 8.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please confirm your password.")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage="Not match with the password")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }


    }
}