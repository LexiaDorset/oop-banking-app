using epita_ca1_74526.Data;
using epita_ca1_74526.Interfaces;
using epita_ca1_74526.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace epita_ca1_74526.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IAccountRepository _accountRepository;

        public AccountController(
            IAccountRepository accountRepository) 
        {
            _accountRepository = accountRepository;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Account> accounts = await _accountRepository.GetAll();
            return View(accounts);
        }

        public async Task<IActionResult> Detail(int id) 
        { 
            Account account = await _accountRepository.GetByIdAsync(id);
            return View(account);
        }

        
    }
}
