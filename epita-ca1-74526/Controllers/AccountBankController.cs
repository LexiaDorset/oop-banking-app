using epita_ca1_74526.Data;
using epita_ca1_74526.Interfaces;
using epita_ca1_74526.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace epita_ca1_74526.Controllers
{
    public class AccountBankController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IAccountBankRepository _accountRepository;

        public AccountBankController(
            IAccountBankRepository accountRepository) 
        {
            _accountRepository = accountRepository;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<AccountBank> accounts = await _accountRepository.GetAll();
            return View(accounts);
        }

        public async Task<IActionResult> Detail(int id) 
        { 
            AccountBank account = await _accountRepository.GetByIdAsync(id);
            return View(account);
        }

        
    }
}
