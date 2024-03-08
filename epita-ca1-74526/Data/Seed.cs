using epita_ca1_74526.Data.Enum;
using epita_ca1_74526.Models;
using Microsoft.AspNetCore.Identity;

namespace epita_ca1_74526.Data
{
    public class Seed
    {
       /* public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();

                //Accounts
                if (!context.AccountsBank.Any())
                {
                    context.AccountsBank.AddRange(new List<AccountBank>()
                    {
                        new AccountBank()
                        {
                            accountType = AccountType.Checking,
                            Name = "ls-11-12-16"
                        },
                        new AccountBank()
                        {
                            accountType = AccountType.Saving,
                            Name = "ls-11-12-16"
                        }
                    });
                    context.SaveChanges();
                }
                //Accounts
                if (!context.Transactions.Any())
                {
                    context.Transactions.AddRange(new List<Transaction>()
                    {
                        new Transaction()
                        {
                            Amount = 250,
                            Date = DateTime.Now,
                            transactionType = TransactionType.Transferred,
                            AccountId = 1,
                            UserId= 3,
                            Title = "test",
                            Balance = 250
                        }
                    });
                    context.SaveChanges();
                }
            }
        }*/
        
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();

                
                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole<int>(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole<int>(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminUserEmail = "74526@student.dorset-college.ie";
                userManager.Options.Password.RequireUppercase = false;
                userManager.Options.Password.RequiredLength = 2;
                userManager.Options.Password.RequireNonAlphanumeric = false;
                userManager.Options.Password.RequireDigit = false;
                userManager.Options.Password.RequireLowercase = false;
                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        firstName = "Lucile",
                        lastName = "Pelou",
                        accountNumber = "ls-11-12-16",
                        pin = "A1234",
                        UserName = "lucilepelou",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                    };
                    await userManager.CreateAsync(newAdminUser, "A1234");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }
            }
        }
    }
}