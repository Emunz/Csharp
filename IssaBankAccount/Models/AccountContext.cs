using Microsoft.EntityFrameworkCore;
 
namespace IssaBankAccount.Models
{
    public class AccountContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public AccountContext(DbContextOptions<AccountContext> options) : base(options) { }
        public DbSet<Account> accounts { get; set; }
    }
}
