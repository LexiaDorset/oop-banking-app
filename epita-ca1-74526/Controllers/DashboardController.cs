using epita_ca1_74526.Data;
using epita_ca1_74526.Interfaces;
using epita_ca1_74526.Models;
using epita_ca1_74526.Repository;
using epita_ca1_74526.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace epita_ca1_74526.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;
        private readonly IUserRepository _userRepository;
        private readonly SignInManager<AppUser> _signInManager;

        public IHttpContextAccessor _httpContextAccessor { get; }


        public DashboardController(IDashboardRepository dashboardRepository,
            IHttpContextAccessor httpContextAccessor, IUserRepository userRepository,
                        SignInManager<AppUser> signInManager)
        {
            _dashboardRepository = dashboardRepository;
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
            _signInManager = signInManager;
        }


        /// Action method for the dashboard index page.
        /// <returns>The view for the dashboard index page.</returns>
        public async Task<IActionResult> Index()
        {
            var userAccountsBank = await _dashboardRepository.GetAllUserAccountsBank();
            var userTransactions = await _dashboardRepository.GetAllUserTransactions();
            var curUser = _httpContextAccessor.HttpContext.User.GetUserId();
            var user = await _userRepository.GetUserById(curUser);
            if (user == null)
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Login", "Account");
            }
            var dashboardViewModel = new DashboardViewModel()
            {
                AccountsBank = userAccountsBank,
                Transactions = userTransactions,
                user = user
            };
            return View(dashboardViewModel);
        }
    }
}
