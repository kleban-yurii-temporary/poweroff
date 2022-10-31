using Microsoft.AspNetCore.Mvc;
using PowerOff.Host.Models;
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

        [Route("/streets/list")]
        public async Task<IEnumerable<StreetForList>> GetList()
        {
            return (await streetsRepository.GetListAsync(await sessionManager.GetLocalityId())).Select(x => new StreetForList
            {
                StreetId = x.Id,
                StreetName = $"{x.Type.ShortTitle} {x.Title}"
            });
        }
    }
}
