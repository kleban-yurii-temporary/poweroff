using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PowerOff.Repositories;

namespace PowerOff.Host.Controllers
{    
    public class EventsController : Controller
    {
        private readonly EventRepository eventRepository;
        private readonly SessionManager sessionManager;
        private readonly AccessHelperRepository accessHelperRepository;

        public EventsController(EventRepository eventRepository, 
            SessionManager sessionManager,
            AccessHelperRepository accessHelperRepository)
        {
            this.eventRepository = eventRepository;
            this.sessionManager = sessionManager;
            this.accessHelperRepository = accessHelperRepository;
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

            return View();
        }
    }
}
