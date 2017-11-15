using Microsoft.EntityFrameworkCore;

namespace BankAccount.Models
{
    public class UsersContext : DbContext
    {
        // Other code
    
        // This DbSet contains "Person" objects and is called "Users"
        public DbSet<User> Users { get; set; }
        public UsersContext(DbContextOptions<UsersContext> options) : base(options) { }
    }
}
