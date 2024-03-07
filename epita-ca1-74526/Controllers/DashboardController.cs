using epita_ca1_74526.Data;
using epita_ca1_74526.Interfaces;
using epita_ca1_74526.Repository;
using epita_ca1_74526.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace epita_ca1_74526.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;
        private readonly IUserRepository _userRepository;

        public IHttpContextAccessor _httpContextAccessor { get; }


        public DashboardController(IDashboardRepository dashboardRepository,
            IHttpContextAccessor httpContextAccessor, IUserRepository userRepository) 
        {
            _dashboardRepository = dashboardRepository;
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }


        public async Task<IActionResult> Index()
        {
            var userAccountsBank = await _dashboardRepository.GetAllUserAccountsBank();
            var userTransactions = await _dashboardRepository.GetAllUserTransactions();
            var curUser = _httpContextAccessor.HttpContext.User.GetUserId();
            var user = await _userRepository.GetUserById(curUser);
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
