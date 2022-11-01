using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PowerOff.Host.Models;
using PowerOff.Repositories;
using System.Globalization;
using System.Text.Json;

namespace PowerOff.Host.Controllers
{    
    public class EventsController : Controller
    {
        private readonly EventRepository eventRepository;
        private readonly SessionManager sessionManager;
        private readonly AccessHelperRepository accessHelperRepository;
        private readonly LocalityRepository localityRepository;
        public EventsController(EventRepository eventRepository, 
            SessionManager sessionManager,
            AccessHelperRepository accessHelperRepository,
            LocalityRepository localityRepository)
        {
            this.eventRepository = eventRepository;
            this.sessionManager = sessionManager;
            this.accessHelperRepository = accessHelperRepository;
            this.localityRepository = localityRepository;
        }

        public async Task<IActionResult> Index()
        {
            var localityId = await sessionManager.GetLocalityId();
            var data = await eventRepository.GetListAsync(localityId);
            ViewBag.EditAllowed = User.Identity.IsAuthenticated ? await accessHelperRepository.LocalityAccessAllowed(localityId, User.Identity.Name) : false;
            return View(data);
        }

        [Authorize(Roles = "Moderator")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Locality = await localityRepository.GetLocalityByUserAsync(User.Identity.Name);
            return View();
        }


        [Authorize(Roles = "Moderator")]
        [HttpPost]
        public async Task<IActionResult> Create(IFormCollection data)
        {
            DateTime start = DateTime.ParseExact($"{data["dateStart"]} {data["timeStart"]}", "yyyy-MM-dd HH:mm", new CultureInfo("en"));
            DateTime end = DateTime.ParseExact($"{data["dateEnd"]} {data["timeEnd"]}", "yyyy-MM-dd HH:mm", new CultureInfo("en"));
            var streets = JsonSerializer.Deserialize<IEnumerable<StreetForList>>(data["streetsList"]).ToList();
            var streetIds = streets.Select(x => Convert.ToInt32(x.StreetId));
            var locality = await localityRepository.GetLocalityByUserAsync(User.Identity.Name);

            await eventRepository.CreateAsync(locality.Id, start, end, streetIds);

            return RedirectToAction("Index");
        }
    }
}
