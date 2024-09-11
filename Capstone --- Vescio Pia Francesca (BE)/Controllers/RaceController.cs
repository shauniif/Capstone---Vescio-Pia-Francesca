using Capstone_____Vescio_Pia_Francesca__BE_.Entity;
using Capstone_____Vescio_Pia_Francesca__BE_.Services.Classes;
using Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces;
using Capstone_____Vescio_Pia_Francesca__BE_.Views;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Controllers
{

    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Authorize(Policies.IsSubAdminOrAdmin)]
    public class RaceController : Controller
    {
        private readonly IRacesService _raceSvc;

        public RaceController(IRacesService raceSvc)
        {
            _raceSvc = raceSvc;

        }
        public async Task<IActionResult> AllRaces()
        {
            var races = await _raceSvc.GetAllRaces();
            return View(races);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Race race)
        {
            if (ModelState.IsValid)
            {
                await _raceSvc.Create(race);
                return RedirectToAction(nameof(AllRaces));
            }
            else
            {
                return View(race);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var race = await _raceSvc.Read(id);
            return View(race);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Race race)
        {
            if (ModelState.IsValid)
            {
                await _raceSvc.Update(race);
                return RedirectToAction(nameof(AllRaces));

            }
            else
            {
                return View(race);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var race = await _raceSvc.Delete(id);
            return RedirectToAction(nameof(AllRaces));
        }
    }
}
