using Microsoft.EntityFrameworkCore;
using System;
 
namespace restauranter.Models
{
    public class ReviewsContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public ReviewsContext(DbContextOptions<ReviewsContext> options) : base(options) { }

        public DbSet<Review> reviews { get; set; }
    }
}