using Microsoft.EntityFrameworkCore;

namespace BeltExam.Models
{
    public class BeltContext : DbContext
    {
        // Other code
    
        // This DbSet contains "Person" objects and is called "Users"
        public DbSet<User> Users { get; set; }
        public DbSet<Connection> Connections { get; set; }
        public BeltContext(DbContextOptions<BeltContext> options) : base(options) { }
    }
}
