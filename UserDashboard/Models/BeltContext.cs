using Microsoft.EntityFrameworkCore;

namespace UserDashboard.Models
{
    public class DashboardContext : DbContext
    {
        // Other code
    
        // This DbSet contains "Person" objects and is called "Users"
        public DbSet<User> Users { get; set; }
        public DashboardContext(DbContextOptions<DashboardContext> options) : base(options) { }
    }
}
