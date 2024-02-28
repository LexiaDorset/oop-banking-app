using epita_ca1_74526.Data;
using epita_ca1_74526.Interfaces;
using epita_ca1_74526.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace epita_ca1_74526.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionController(ITransactionRepository transactionRepository) 
        {
            _transactionRepository = transactionRepository;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Transaction> transactions = await _transactionRepository.GetAll();
            return View(transactions);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Transaction transaction = await _transactionRepository.GetByIdAsync(id);
            return View(transaction);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Transaction transaction)
        {
            if(!ModelState.IsValid)
            {
                return View(transaction);
            }
            _transactionRepository.Add(transaction);
            return RedirectToAction("Index");
        }
    }
}
