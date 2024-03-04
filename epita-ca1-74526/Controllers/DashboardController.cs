using epita_ca1_74526.Data;
using epita_ca1_74526.Interfaces;
using epita_ca1_74526.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace epita_ca1_74526.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardController(IDashboardRepository dashboardRepository) 
        {
            _dashboardRepository = dashboardRepository;
        }
        public async Task<IActionResult> Index()
        {
            var userAccountsBank = await _dashboardRepository.GetAllUserAccountsBank();
            var userTransactions = await _dashboardRepository.GetAllUserTransactions();
            var dashboardViewModel = new DashboardViewModel()
            {
                AccountsBank = userAccountsBank,
                Transactions = userTransactions
            };
            return View(dashboardViewModel);
        }
    }
}
