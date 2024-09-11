using Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces;
using Capstone_____Vescio_Pia_Francesca__BE_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Capstone_____Vescio_Pia_Francesca__BE_.Views;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Authorize(Policies.IsSubAdminOrAdmin)]
    public class EventController : Controller
    {
        private readonly IEventService _eventSvc;

        public EventController(IEventService eventSvc)
        {
            _eventSvc = eventSvc;

        }

        
        public async Task<IActionResult> AllEvents()
        {
            var events = await _eventSvc.GetAll();
            return View(events);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EventModel newEvent)
        {
            if (ModelState.IsValid) {
                {
                    await _eventSvc.Create(newEvent);
                    return RedirectToAction(nameof(AllEvents));
                } 
            }
                return View(newEvent);
        }

        public async Task<IActionResult> Delete(int id) 
        {
            await _eventSvc.Delete(id);
            return RedirectToAction(nameof(AllEvents));
        }

        public async Task<IActionResult> Edit(int id) 
        { 
            var eventModel = await _eventSvc.Get(id);
            return View(eventModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EventModel currEvent)
        {
            if (ModelState.IsValid)
            {
                await _eventSvc.Update(currEvent);
                return RedirectToAction(nameof(AllEvents));
            }
            return View(currEvent);
        }
    }
}
