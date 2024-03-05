using epita_ca1_74526.Data;
using epita_ca1_74526.Interfaces;
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

        public UserController(IUserRepository userRepository, ApplicationDbContext context)
        {
            _userRepository = userRepository;
            _context = context;
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
                AccountsBank = userAccountsBank.ToList(),
                Transactions = userTransactions.ToList()
            };
            return View(userDetailViewModel);
        }

    }
}
