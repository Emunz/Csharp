using Microsoft.EntityFrameworkCore;
 
namespace IssaBankAccount.Models
{
    public class UsersContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public UsersContext(DbContextOptions<UsersContext> options) : base(options) { }
        public DbSet<User> users { get; set; }
    }
}