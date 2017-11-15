using System.ComponentModel.DataAnnotations;
using System;
 
namespace LoginRegistration.Models
{
    public class User
    {
        [Required]
        [MinLength(2)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
 
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }
 
        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage="Make sure your passwords are matching!")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}