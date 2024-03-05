using epita_ca1_74526.Data;
using epita_ca1_74526.Interfaces;
using epita_ca1_74526.Models;
using epita_ca1_74526.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace epita_ca1_74526.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly IAccountBankRepository _accountRepository;

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
        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if(!ModelState.IsValid) 
            {
                return View(loginViewModel);
            }
            var user = await _userManager.FindByNameAsync(loginViewModel.firstName + loginViewModel.lastName);

            if(user != null)
            {
                // User is found, check password and sign if good
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
                if(passwordCheck && user.accountNumber == loginViewModel.NameAccount)
                {
                    // Password is ok so sign in
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                    if(result.Succeeded)
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

        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            // Check if the model state is valid
            if(!ModelState.IsValid) 
            {
                return View(registerViewModel);
            }
            var user = await _userManager.FindByEmailAsync(registerViewModel.EmailAddress);
            if(user != null) 
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
                pin = registerViewModel.pin,
                accountNumber = registerViewModel.NameAccount
            };

            var newUserResponse = await _userManager.CreateAsync(newUser, newUser.pin);
            
            if(newUserResponse.Succeeded)
            {
                newUser.pin += "+" + newUser.Id;
               
                // Update User with good pin
                await _userManager.UpdateAsync(newUser);

                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
                _accountRepository.Add(registerViewModel.createAccountsBank(Data.Enum.AccountType.Saving, newUser.Id));
                _accountRepository.Add(registerViewModel.createAccountsBank(Data.Enum.AccountType.Checking, newUser.Id));
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
