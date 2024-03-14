using epita_ca1_74526.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
// Name: Lucile Pelou
// Student number: 74526
namespace epita_ca1_74526.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<AccountBank> AccountsBank { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

    }
}
