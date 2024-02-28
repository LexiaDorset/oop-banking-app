using epita_ca1_74526.Data.Enum;
using epita_ca1_74526.Models;
using Microsoft.AspNetCore.Identity;

namespace epita_ca1_74526.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();

                if (!context.Users.Any())
                {
                    context.Users.AddRange(new List<AppUser>()
                    {
                        new AppUser()
                        {
                            firstName = "Lucile",
                            lastName =  "Pelou",
                            accountNumber = "ls-11-12-16",
                            pin = "ls-11-12-16+1"
                        }
                    });
                    context.SaveChanges();
                }
                //Accounts
                if (!context.Accounts.Any())
                {
                    context.Accounts.AddRange(new List<Account>()
                    {
                        new Account()
                        {
                            accountType = AccountType.Checking,
                            Name = "ls-11-12-16"
                        },
                        new Account()
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
                            UserId=1,
                            Title = "test"
                        }
                    });
                    context.SaveChanges();
                }
            }
        }
        /*
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminUserEmail = "teddysmithdeveloper@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        UserName = "teddysmithdev",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        Address = new Address()
                        {
                            Street = "123 Main St",
                            City = "Charlotte",
                            State = "NC"
                        }
                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "user@etickets.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        Address = new Address()
                        {
                            Street = "123 Main St",
                            City = "Charlotte",
                            State = "NC"
                        }
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }*/
    }
}