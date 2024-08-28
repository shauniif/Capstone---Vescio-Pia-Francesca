using Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces;
using Capstone_____Vescio_Pia_Francesca__BE_.Models;
using Microsoft.AspNetCore.Mvc;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService _eventSvc;

        public EventController(IEventService eventSvc)
        {
            _eventSvc = eventSvc;

        }
        public async Task<IActionResult> AllEvents()
        {
            var events = await _eventSvc.GetAllView();
            return View(events);
        }

        public IActionResult Create() { 
            return View();
        }

        [HttpPost]
        public IActionResult Create(EventModel newEvent) {

            
                _eventSvc.Create(newEvent);
                return RedirectToAction(nameof(AllEvents));

        }
    }
}
