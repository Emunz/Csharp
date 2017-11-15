using System;
using System.ComponentModel.DataAnnotations;

namespace WeddingPlanner.Models
{
    public class WeddingViewModel : BaseEntity
    {
        [Required(ErrorMessage = "Wedder One is required")]
        public string WedderOne { get; set; }

        [Required(ErrorMessage = "Wedder Two is required")]
        public string WedderTwo { get; set; }

        [Required(ErrorMessage = "Data is required")]
        [DisplayFormat(DataFormatString = "{0:D}")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
    }
}