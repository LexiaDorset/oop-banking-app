using epita_ca1_74526.Data;
using epita_ca1_74526.Interfaces;
using epita_ca1_74526.Models;
using epita_ca1_74526.Repository;
using epita_ca1_74526.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace epita_ca1_74526.Controllers
{
    public class AccountBankController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IAccountBankRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;


        public AccountBankController(
            IAccountBankRepository accountRepository,
            ITransactionRepository transactionRepository) 
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<AccountBank> accounts = await _accountRepository.GetAll();
            return View(accounts);
        }

        public async Task<IActionResult> Detail(int id) 
        { 
            AccountBank account = await _accountRepository.GetByIdAsync(id);
            if(account == null)
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
