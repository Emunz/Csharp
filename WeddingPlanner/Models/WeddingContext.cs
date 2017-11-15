using Microsoft.EntityFrameworkCore;

namespace WeddingPlanner.Models
{
    public class WeddingContext : DbContext
    {
        // Other code
    
        // This DbSet contains "Person" objects and is called "Users"
        public DbSet<User> Users { get; set; }
        public DbSet<Wedding> Weddings { get; set;}
        public DbSet<Guest> Guests { get; set; }
        public WeddingContext(DbContextOptions<WeddingContext> options) : base(options) { }
    }
}
