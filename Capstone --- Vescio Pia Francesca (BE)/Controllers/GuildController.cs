using Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces;
using Capstone_____Vescio_Pia_Francesca__BE_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Capstone_____Vescio_Pia_Francesca__BE_.Entity;
using Capstone_____Vescio_Pia_Francesca__BE_.Views;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Authorize(Policies.IsSubAdminOrAdmin)]
    public class GuildController : Controller
    {
            private readonly IGuildService _guildSvc;
            private readonly INationService _nationSvc;
            public GuildController(IGuildService guildSvc, INationService nationSvc)
            {
                _guildSvc = guildSvc;
                _nationSvc = nationSvc;

            }
            public async Task<IActionResult> AllGuilds()
            {
                var guilds = await _guildSvc.GetAll();
                return View(guilds);
            }

            public async Task<IActionResult> Create() 
            {
                var nation = await _nationSvc.GetAll();
                ViewBag.Nations = nation;
                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(GuildModel guild)
            {
                if(ModelState.IsValid) {
                    await _guildSvc.Create(guild);
                    return RedirectToAction(nameof(AllGuilds));
                }
                else {
                    var nations = await _nationSvc.GetAll();
                    ViewBag.Nations = nations;
                    return View(guild);
                }
                
            }

            public async Task<IActionResult> Delete(int id)
            {
                 await _guildSvc.Delete(id);
                return RedirectToAction(nameof(AllGuilds));
            }

            public async Task<IActionResult> Edit(int id) 
            {
                var guild = await _guildSvc.Get(id);
                var nations = await _nationSvc.GetAll();
                ViewBag.Nations = nations;
                return View(guild);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(GuildModel guild)
            {
                if (ModelState.IsValid)
                {
                    await _guildSvc.Update(guild);
                    return RedirectToAction(nameof(AllGuilds));
                }
                else
                {
                    var nation = await _nationSvc.GetAll();
                    ViewBag.Nations = nation;
                    return View(guild);
                }
            
        }

        public async Task<IActionResult> Detail(int id)
        {
            var guild = await _guildSvc.Read(id);
            return View(guild);
        }

    }
}
