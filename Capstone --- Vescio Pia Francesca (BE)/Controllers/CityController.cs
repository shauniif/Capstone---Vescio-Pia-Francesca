using Capstone_____Vescio_Pia_Francesca__BE_.Entity;
using Capstone_____Vescio_Pia_Francesca__BE_.Models;
using Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Controllers
{
    public class CityController : Controller
    {
        private readonly ICitiesService _citiesSvc;
        private readonly INationService _nationSvc;

        public CityController(ICitiesService citiesSvc, INationService nationSvc)
        {
            _citiesSvc = citiesSvc;
            _nationSvc = nationSvc;

        }
        public async Task<IActionResult> AllCities()
        {
            var cities = await _citiesSvc.GetAllCities();
            return View(cities);
        }

        public async Task<IActionResult> Create()
        {
            var nation = await _nationSvc.GetAllNations();
            ViewBag.Nations = nation;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CityModel city, int nationId)
        {

            if(ModelState.IsValid)
            {
                await _citiesSvc.Create(city);
                return RedirectToAction(nameof(AllCities));
            }
            else
            {
                var nation = await _nationSvc.GetAllNations();
                ViewBag.Nations = nation;
                return View(city);
            }
        } 

        public async Task<IActionResult> Edit(int id)
        {
            var city = await _citiesSvc.Get(id);
            var nations = await _nationSvc.GetAllNations();
            ViewBag.Nations = new SelectList(nations, "Id", "Name", city.NationId);
            return View(city);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CityModel city)
        {
            if (ModelState.IsValid)
            {
                await _citiesSvc.Update(city);
                return RedirectToAction(nameof(AllCities));
            }
            else
            {
                var nations = await _nationSvc.GetAllNations();
                ViewBag.Nations = new SelectList(nations, "Id", "Name", city.NationId);
                return View(city);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _citiesSvc.Delete(id);
            return RedirectToAction(nameof(AllCities));
        }
    }
}
