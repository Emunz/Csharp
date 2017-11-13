using System;
using System.ComponentModel.DataAnnotations;

namespace restauranter.Models
{
    public class Review
    {
        [Key]
        public int id { get; set; }

        [Required]
        [MinLength(2)]
        [Display(Name="Reviewer Name")]
        public string reviewer_name { get; set; }

        [Required]
        [MinLength(2)]
        [Display(Name="Restaurant Name")]
        public string restaurant_name { get; set; }

        [Required]
        [MinLength(10)]
        [Display(Name="Review")]
        public string review { get; set; }

        [Required]
        [Display(Name="Date of Visit")]
        public DateTime date { get; set; }

        [Required]
        [Display(Name="Stars")]
        public int stars { get; set; }
    }
}