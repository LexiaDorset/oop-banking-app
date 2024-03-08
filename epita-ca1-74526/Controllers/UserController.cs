using epita_ca1_74526.Data;
using epita_ca1_74526.Interfaces;
using epita_ca1_74526.Models;
using epita_ca1_74526.Repository;
using epita_ca1_74526.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace epita_ca1_74526.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ApplicationDbContext _context;
        private readonly IAccountBankRepository _accountBankRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SignInManager<AppUser> _signInManager;

        public UserController(IUserRepository userRepository, ApplicationDbContext context,
             IAccountBankRepository accountBankRepository, ITransactionRepository transactionRepository,
             UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor,
             SignInManager<AppUser> signInManager)
        {
            _userRepository = userRepository;
            _context = context;
            _accountBankRepository = accountBankRepository;
            _transactionRepository = transactionRepository;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _signInManager = signInManager;
        }

        [HttpGet("users")]
        public async Task<IActionResult> Index()
        {
            var curUser = _httpContextAccessor.HttpContext.User.GetUserId();
            var userCheck = await _userRepository.GetUserById(curUser);
            if (userCheck == null)
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Login", "Account");
            }
            var users = await _userRepository.GetAllUsersUser();

            List<UserViewModel> result = new List<UserViewModel>();
            foreach (var user in users)
            {
                var userAccountsBank = _context.AccountsBank.Where(r => r.UserId == user.Id);
                var userViewModel = new UserViewModel()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    AccountsBank = userAccountsBank.ToList()
                };
                result.Add(userViewModel);  
            }
            return View(result);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                return RedirectToAction("Error", "Home");
            }
            var userAccountsBank = _context.AccountsBank.Where(r => r.UserId == id);
            var userTransactions = _context.Transactions.Where(r => r.UserId == id);

            var userDetailViewModel = new UserDetailViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                AccountsBank = userAccountsBank.ToList(),
                Transactions = userTransactions.ToList(),
                Balance = user.Balance
            };
            return View(userDetailViewModel);
        }

        public async Task<IActionResult> CreateAdmin(int id)
        {
            var userAccounts = await _accountBankRepository.GetByUserIdAsync(id);
            if (userAccounts.Count() == 0)
            {
                return RedirectToAction("Index", "User");
            }
            var viewModel = new CreateAdminTransactionViewModel
            {
                UserAccounts = userAccounts
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAdmin(int id,  CreateAdminTransactionViewModel createAdminTransactionViewModel)
        {
            if(createAdminTransactionViewModel.transactionType == null
                && createAdminTransactionViewModel.Title == null &&
                createAdminTransactionViewModel.AccountId == 0)
            {
                return View(createAdminTransactionViewModel);
            }
            var Transaction = new Transaction();
            Transaction.AccountId = createAdminTransactionViewModel.AccountId;
            var transactionAccount = await _accountBankRepository.
                GetByIdAsync((int)Transaction.AccountId);
            Transaction.Title = createAdminTransactionViewModel.Title;
            Transaction.transactionType = createAdminTransactionViewModel.transactionType;
            Transaction.Amount = createAdminTransactionViewModel.Amount;
            var user = await _userRepository.GetUserById(id);
            Transaction.Process(transactionAccount, user);
            Transaction.UserId = id;
            _transactionRepository.Add(Transaction);
            return RedirectToAction("Detail", "User", new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userRepository.GetUserById(id);
            if(user == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            var user = await _userRepository.GetUserById(id);

            if (user == null)
            {
                TempData["Error"] = "This user doesn't exists!";

                return View(user);
            }
            if(user.Balance > 0)
            {
                TempData["Error"] = "You cannot delete an account with more than zero in balance";
                return View(user);
            }
            else
            {
                var userAccounts = await _accountBankRepository.GetByUserIdAsync(id);
                foreach (var userAccount in userAccounts)
                {
                    var transactionAccounts = await _transactionRepository.GetByAccountIdAsync(userAccount.Id);
                    foreach (var transaction in transactionAccounts)
                    {
                        _transactionRepository.Delete(transaction);

                    }
                    _accountBankRepository.Delete(userAccount);
                }
                var newUserResponse = await _userManager.DeleteAsync(user);


                return RedirectToAction("Index", "User");
            }           
        }
    }
}
