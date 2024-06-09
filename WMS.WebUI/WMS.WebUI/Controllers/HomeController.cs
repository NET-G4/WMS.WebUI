using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WMS.WebUI.Models;
using WMS.WebUI.Stores.Interfaces;

namespace WMS.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDashboardStore _dashboardStore;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IDashboardStore dashboardStore)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _dashboardStore = dashboardStore ?? throw new ArgumentNullException(nameof(dashboardStore));
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            var dashboard = _dashboardStore.Get();

            return View(dashboard);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
