using epita_ca1_74526.Data;
using epita_ca1_74526.Interfaces;
using epita_ca1_74526.Models;
using epita_ca1_74526.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
// Name: Lucile Pelou
// Student number: 74526
namespace epita_ca1_74526.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly IAccountBankRepository _accountRepository;

        /// Initializes a new instance of the <see cref="AccountController"/> class.
        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ApplicationDbContext context, IAccountBankRepository accountRepository)
        {
            _accountRepository = accountRepository;

            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _userManager.Options.Password.RequireUppercase = false;
            _userManager.Options.Password.RequiredLength = 2;
            _userManager.Options.Password.RequireNonAlphanumeric = false;
            _userManager.Options.Password.RequireDigit = false;
            _userManager.Options.Password.RequireLowercase = false;

        }

        /// Displays the login view.
        /// <returns>The login view.</returns>
        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        /// Handles the login form submission.
        /// <param name="loginViewModel">The login view model.</param>
        /// <returns>The result of the login attempt.</returns>
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid && loginViewModel.SelectedRole != "Admin")
            {
                return View(loginViewModel);
            }
            if (loginViewModel.SelectedRole != "Admin")
            {
                if (loginViewModel.firstName + loginViewModel.lastName != "lucilepelou")
                {
                    var user = await _userManager.FindByNameAsync(loginViewModel.firstName + loginViewModel.lastName);

                    if (user != null)
                    {
                        // User is found, check password and sign if good
                        var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
                        if (passwordCheck)
                        {
                            // Password is ok so sign in
                            var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                            if (result.Succeeded)
                            {
                                return RedirectToAction("Index", "Dashboard");
                            }
                        }
                        // Password or email is wrong
                        TempData["Error"] = "Wrong password, email or account number.";
                        return View(loginViewModel);
                    }
                    // User doesn't exists
                    TempData["Error"] = "This User doesn't exists.";
                    return View(loginViewModel);
                }
                return View(loginViewModel);
            }
            else if (loginViewModel.Password != null)
            {
                var user = await _userManager.FindByNameAsync("lucilepelou");

                if (user != null)
                {
                    // User is found, check password and sign if good
                    var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
                    if (passwordCheck)
                    {
                        // Password is ok so sign in
                        var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("Index", "User");
                        }
                    }
                    // Password or email is wrong
                    TempData["Error"] = "Wrong password!";
                    return View(loginViewModel);
                }
                // User doesn't exists
                TempData["Error"] = "There is no admin here. Huh Strange...";
                return View(loginViewModel);
            }
            return View(loginViewModel);
        }

        /// Displays the registration view.
        /// <returns>The registration view.</returns>
        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response);
        }

        /// Handles the registration form submission.
        /// <param name="registerViewModel">The register view model.</param>
        /// <returns>The result of the registration attempt.</returns>
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            // Check if the model state is valid
            if (!ModelState.IsValid)
            {
                return View(registerViewModel);
            }
            var user = await _userManager.FindByEmailAsync(registerViewModel.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(registerViewModel);
            }
            registerViewModel.createPinAndName();


            var newUser = new AppUser()
            {
                Email = registerViewModel.EmailAddress,
                firstName = registerViewModel.firstName,
                lastName = registerViewModel.lastName,
                UserName = registerViewModel.firstName + registerViewModel.lastName,
                pin = registerViewModel.pin
            };

            var newUserResponse = await _userManager.CreateAsync(newUser, newUser.pin);

            if (newUserResponse.Succeeded)
            {

                newUser.pin += "+" + newUser.Id;

                // Update User with good pin
                await _userManager.UpdateAsync(newUser);
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);

                // Remove the old password
                await _userManager.RemovePasswordAsync(newUser);
                // Add the new user password with the good id
                await _userManager.AddPasswordAsync(newUser, newUser.pin);

                _accountRepository.Add(registerViewModel.createAccountsBank(Data.Enum.AccountType.Saving, newUser.Id));
                _accountRepository.Add(registerViewModel.createAccountsBank(Data.Enum.AccountType.Checking, newUser.Id));
            }
            return RedirectToAction("Index", "User");
        }

        /// Handles the logout action.
        /// <returns>Redirect to home.</returns>
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
