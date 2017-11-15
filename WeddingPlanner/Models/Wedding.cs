using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace WeddingPlanner.Models
{
    public class Wedding : BaseEntity
    {
        [Key]
        public int WeddingId { get; set; }
        public int HostId { get; set; }
        public User Host { get; set;}
        public string WedderOne { get; set; }
        public string WedderTwo { get; set; }

        [DisplayFormat(DataFormatString = "{0:D}")]
        public DateTime Date { get; set; }
        public string Address { get; set; }
        public List<Guest> Guests { get; set; }

        public Wedding()
        {
            Guests = new List<Guest>();
        }
        
    }
}