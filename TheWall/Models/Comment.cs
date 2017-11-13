using System.ComponentModel.DataAnnotations;
using System;
 
namespace LoginRegistration.Models
{
    public class Comment
    {
        [Required]
        [MinLength(2)]
        [Display(Name = "UserComment")]
        public string UserComment { get; set; }
    }
}