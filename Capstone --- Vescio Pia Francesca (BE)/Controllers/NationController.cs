using Capstone_____Vescio_Pia_Francesca__BE_.Entity;
using Capstone_____Vescio_Pia_Francesca__BE_.Models;
using Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Buffers.Text;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Controllers
{
    public class NationController : Controller
    {
        private readonly INationService _nationSvc;

        public NationController(INationService nationSvc)
        {
            _nationSvc = nationSvc;

        }
        public async Task<IActionResult> AllNations()
        {
            var nations = await _nationSvc.GetAllNations();
            return View(nations);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NationModel nations)
        {
            if (ModelState.IsValid)
            {
                await _nationSvc.Create(nations);
                return RedirectToAction(nameof(AllNations));
            }
            else
            {
                return View(nations);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var nation = await _nationSvc.Get(id);
            return View(nation);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(NationModel nation)
        {
            if (ModelState.IsValid)
            {
                await _nationSvc.Update(nation);
                return RedirectToAction(nameof(AllNations));
            }
            else
            {
                return View(nation);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var race = await _nationSvc.Delete(id);
            return RedirectToAction(nameof(AllNations));
        }

        public async Task<IActionResult> Detail(int  id)
        {
            var nation = await _nationSvc.Get(id);
            return View(nation);
        }
    }
}
