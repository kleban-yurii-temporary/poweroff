using Microsoft.AspNetCore.Mvc;
using PowerOff.Repositories;

namespace PowerOff.Host.Controllers
{
    public class StreetsController : Controller
    {
        private readonly StreetsRepository streetsRepository;
        private readonly SessionManager sessionManager;
        private readonly AccessHelperRepository accessHelperRepository;

        public StreetsController(StreetsRepository streetsRepository, 
            SessionManager sessionManager, 
            AccessHelperRepository accessHelperRepository)
        {
            this.streetsRepository = streetsRepository;
            this.sessionManager = sessionManager;
            this.accessHelperRepository = accessHelperRepository;
        }

        public async Task<IActionResult> Index()
        {
            var data = await streetsRepository.GetListAsync(await sessionManager.GetLocalityId());
            ViewBag.EditAllowed = User.Identity.IsAuthenticated ? await accessHelperRepository.StreetAccessAllowed(data.First().Id, User.Identity.Name) : false;
            return View(data);
        }
    }
}
