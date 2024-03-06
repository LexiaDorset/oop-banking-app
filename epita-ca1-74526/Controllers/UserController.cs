using epita_ca1_74526.Data;
using epita_ca1_74526.Interfaces;
using epita_ca1_74526.Models;
using epita_ca1_74526.Repository;
using epita_ca1_74526.ViewModels;
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

        public UserController(IUserRepository userRepository, ApplicationDbContext context,
             IAccountBankRepository accountBankRepository, ITransactionRepository transactionRepository)
        {
            _userRepository = userRepository;
            _context = context;
            _accountBankRepository = accountBankRepository;
            _transactionRepository = transactionRepository;
        }
        [HttpGet("users")]
        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAllUsersUser();
            List<UserViewModel> result = new List<UserViewModel>();
            foreach (var user in users)
            {
                var userViewModel = new UserViewModel()
                {
                    Id = user.Id,
                    UserName = user.UserName
                };
                result.Add(userViewModel);  
            }
            return View(result);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var user = await _userRepository.GetUserById(id);
            var userAccountsBank = _context.AccountsBank.Where(r => r.UserId == id);
            var userTransactions = _context.Transactions.Where(r => r.UserId == id);

            var userDetailViewModel = new UserDetailViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                AccountsBank = userAccountsBank.ToList(),
                Transactions = userTransactions.ToList()
            };
            return View(userDetailViewModel);
        }

        public async Task<IActionResult> CreateAdmin(int id)
        {
            var userAccounts = await _accountBankRepository.GetByUserIdAsync(id);

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

            Transaction.Process(transactionAccount);
            Transaction.UserId = id;
            _transactionRepository.Add(Transaction);
            return RedirectToAction("Index", "User");
        }
    }
}
