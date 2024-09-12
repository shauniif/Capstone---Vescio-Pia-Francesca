using Capstone_____Vescio_Pia_Francesca__BE_.Entity;
using Capstone_____Vescio_Pia_Francesca__BE_.Models;
using Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces;
using Capstone_____Vescio_Pia_Francesca__BE_.Views;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Authorize(Policies.IsSubAdminOrAdmin)]
    public class EcoController : Controller
    {
        private readonly IEcoService _ecoSvc;
        private readonly INationService _nationSvc;
        public EcoController(IEcoService ecoSvc, INationService nationSvc)
        {
            _ecoSvc = ecoSvc;
            _nationSvc = nationSvc;

        }
        public async Task<IActionResult> AllEcos()
        {
            var ecos = await _ecoSvc.GetAllEcos();
            return View(ecos);
        }
        public async Task<IActionResult> Create()
       {
            var nation = await _nationSvc.GetAllNations();
            ViewBag.Nations = nation;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EcoModel eco)
        {
            if (ModelState.IsValid) {
                await _ecoSvc.Create(eco);
                return RedirectToAction(nameof(AllEcos));
            } 
            else {
                var nation = await _nationSvc.GetAllNations();
                ViewBag.Nations = nation;
                return View(eco);
            };
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _ecoSvc.Delete(id);
            return RedirectToAction(nameof(AllEcos));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var eco = await _ecoSvc.Get(id);
            var nations = await _nationSvc.GetAllNations();
            ViewBag.Nations = nations;
            return View(eco);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EcoModel eco)
        {
            if (ModelState.IsValid) {
                await _ecoSvc.Update(eco);
                return RedirectToAction(nameof(AllEcos));
            }
            else {
                var nations = await _nationSvc.GetAllNations();
                ViewBag.Nations = nations;
                return View(eco);
            };
        }

        
        public async Task<IActionResult> Detail(int id)
        {
            var eco = await _ecoSvc.Read(id);
            return View(eco);
        }

        public async Task<IActionResult> GetEcoImage(int id)
        {
            var nation = await _ecoSvc.Read(id);
            if (nation?.Pic == null)
            {
                return NotFound(); 
            }
            var nationPhotodata = nation.Pic.Substring(23);
            byte[] imageBytes = Convert.FromBase64String(nationPhotodata);
            Console.WriteLine(imageBytes);
            return File(imageBytes, "image/jpeg");
        }
    }
}
