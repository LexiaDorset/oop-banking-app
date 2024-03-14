using epita_ca1_74526.Data;
using epita_ca1_74526.Interfaces;
using epita_ca1_74526.Models;
using epita_ca1_74526.Repository;
using epita_ca1_74526.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
// Name: Lucile Pelou
// Student number: 74526
namespace epita_ca1_74526.Controllers
{
    public class AccountBankController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IAccountBankRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;

        /// Initializes a new instance of the <see cref="AccountBankController"/> class.
        public AccountBankController(
            IAccountBankRepository accountRepository,
            ITransactionRepository transactionRepository)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
        }

        /// Displays the list of all bank accounts.
        /// <returns>The view containing the list of bank accounts.</returns>
        public async Task<IActionResult> Index()
        {
            IEnumerable<AccountBank> accounts = await _accountRepository.GetAll();
            return View(accounts);
        }

        /// Displays the details of a bank account.
        /// <param name="id">The ID of the bank account.</param>
        /// <returns>The view containing the details of the bank account.</returns>
        public async Task<IActionResult> Detail(int id)
        {
            AccountBank account = await _accountRepository.GetByIdAsync(id);
            if (account == null)
            {
                return RedirectToAction("Error", "Home");
            }
            var userTransactions = await _transactionRepository.GetByAccountIdAsync(id);

            var viewModel = new DetailAccountBankViewModel
            {
                TransactionsAccount = userTransactions,
                AccountBank = account
            };

            return View(viewModel);
        }
    }
}
