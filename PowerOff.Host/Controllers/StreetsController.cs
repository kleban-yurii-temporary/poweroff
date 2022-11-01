using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PowerOff.Host.Models;
using PowerOff.Repositories;

namespace PowerOff.Host.Controllers
{
    public class StreetsController : Controller
    {
        private readonly StreetsRepository streetsRepository;
        private readonly SessionManager sessionManager;
        private readonly LocalityRepository localityRepository;
        private readonly AccessHelperRepository accessHelperRepository;

        public StreetsController(StreetsRepository streetsRepository, 
            SessionManager sessionManager, 
            AccessHelperRepository accessHelperRepository,
            LocalityRepository localityRepository)
        {
            this.streetsRepository = streetsRepository;
            this.sessionManager = sessionManager;
            this.accessHelperRepository = accessHelperRepository;
            this.localityRepository = localityRepository;
        }

        public async Task<IActionResult> Index()
        {
            var data = await streetsRepository.GetListAsync(await sessionManager.GetLocalityId());
            ViewBag.EditAllowed = User.Identity.IsAuthenticated ? await accessHelperRepository.StreetAccessAllowed(data.First().Id, User.Identity.Name) : false;
            return View(data);
        }

        [Authorize(Roles = "Moderator, Admin")]
        [Route("/streets/list")]
        public async Task<IEnumerable<StreetForList>> GetList()
        {
            return (await streetsRepository.GetListAsync((await localityRepository.GetLocalityByUserAsync(User.Identity.Name)).Id)).Select(x => new StreetForList
            {
                StreetId = x.Id.ToString(),
                StreetName = $"{x.Type.ShortTitle} {x.Title}"
            });
        }
    }
}
