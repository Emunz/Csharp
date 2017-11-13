using System.ComponentModel.DataAnnotations;
using System;
 
namespace LoginRegistration.Models
{
    public class Message
    {
        [Required]
        [MinLength(2)]
        [Display(Name = "UserMessage")]
        public string UserMessage { get; set; }
    }
}