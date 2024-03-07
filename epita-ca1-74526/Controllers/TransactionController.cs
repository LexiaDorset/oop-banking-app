using epita_ca1_74526.Data;
using epita_ca1_74526.Interfaces;
using epita_ca1_74526.Models;
using epita_ca1_74526.Repository;
using epita_ca1_74526.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace epita_ca1_74526.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAccountBankRepository _accountBankRepository;
        private readonly IUserRepository _userRepository;

        public TransactionController(ITransactionRepository transactionRepository,
            IHttpContextAccessor httpContextAccessor, IAccountBankRepository accountBankRepository,
            IUserRepository userRepository) 
        {
            _transactionRepository = transactionRepository;
            _accountBankRepository = accountBankRepository;
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Transaction> transactions = await _transactionRepository.GetAll();
            return View(transactions);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Transaction transaction = await _transactionRepository.GetByIdAsync(id);
            var accountBankR = new AccountBank();
            if (transaction.AccountId != null)
            {
               accountBankR = await _accountBankRepository.GetByIdAsync((int)transaction.AccountId);
            }
            var viewModel = new DetailTransactionViewModel
            {
                transaction = transaction,
                accountBank = accountBankR
        };
            return View(viewModel);
        }
        public async Task<IActionResult> Create()
        {
            var curUser = _httpContextAccessor.HttpContext.User.GetUserId();
            var userAccounts = await _accountBankRepository.GetByUserIdAsync(curUser);

            var viewModel = new CreateTransactionViewModel
            {
                Transaction = new Transaction(),
                UserAccounts = userAccounts
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Transaction transaction)
        {
            if(!ModelState.IsValid)
            {
                return View(transaction);
            }
            var transactionAccount = await _accountBankRepository.GetByIdAsync((int)transaction.AccountId);
            var curUser = _httpContextAccessor.HttpContext.User.GetUserId();
            var user = await _userRepository.GetUserById(curUser);

            transaction.Process(transactionAccount, user);
            transaction.UserId = curUser;
            _transactionRepository.Add(transaction);
            return RedirectToAction("Index", "Dashboard");
        }

        
    }
}
