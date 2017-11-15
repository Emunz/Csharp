using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace WeddingPlanner.Models
{
    public class Guest : BaseEntity
    {
        [Key]
        public int GuestId { get; set; }
        public int WeddingGuestId { get; set; }
        public User WeddingGuest { get; set;}
        public int WeddingId { get; set; }
        public Wedding Wedding { get; set;}
    }
}