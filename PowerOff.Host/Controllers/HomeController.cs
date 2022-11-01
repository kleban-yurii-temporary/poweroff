using Microsoft.AspNetCore.Mvc;
using PowerOff.Host.Models;
using PowerOff.Repositories;
using System.Diagnostics;

namespace PowerOff.Host.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly LocalityRepository localityRepository;
        private readonly SessionManager sessionManager;

        public HomeController(ILogger<HomeController> logger,
            SessionManager sessionManager, 
            LocalityRepository localityRepository)
        {
            _logger = logger;
            this.sessionManager = sessionManager;
            this.localityRepository = localityRepository;
        }

        public async Task<IActionResult> Index()
        {
            var localityId = await sessionManager.GetLocalityId();
            return View(await localityRepository.GetLocalityAsync(localityId));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}