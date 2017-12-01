using System.ComponentModel.DataAnnotations;
namespace UserDashboard.Models
{
    public class RegisterViewModel : BaseEntity
    {
        [Required(ErrorMessage = "First Name is required")]
        [MinLength(2, ErrorMessage = "First Name must be at least 2 characters")]
        [RegularExpression(@"^[a-zA-Z]+$")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [MinLength(2, ErrorMessage = "Last Name must be at least 2 characters")]
        [RegularExpression(@"^[a-zA-Z]+$")]
        public string LastName { get; set; }
 
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }
 
        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
 
        [Compare("Password", ErrorMessage = "Password and confirmation must match")]
        public string ConfirmPassword { get; set; }
    }
}