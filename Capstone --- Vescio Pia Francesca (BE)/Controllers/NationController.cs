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
        private readonly IEventService _eventSvc;

        public NationController(INationService nationSvc, IEventService eventSvc)
        {
            _nationSvc = nationSvc;
            _eventSvc = eventSvc;

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
        public async Task<IActionResult> Create(NationModel nation)
        {
            if (ModelState.IsValid)
            {
                await _nationSvc.Create(nation);
                return RedirectToAction(nameof(AllNations));
            }
            else
            {
                return View(nation);
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
            var nation = await _nationSvc.Delete(id);
            return RedirectToAction(nameof(AllNations));
        }

        public async Task<IActionResult> Detail(int  id)
        {
            var nation = await _nationSvc.Read(id);
            return View(nation);
        }

        public async Task<IActionResult> GetNationImage(int id) 
        {
            var nation = await _nationSvc.Read(id);
            if (nation?.Photo == null)
            {
                return NotFound(); // O un'immagine placeholder
            }
            var nationPhotodata = nation.Photo.Substring(23);
            byte[] imageBytes = Convert.FromBase64String(nationPhotodata);
            return File(imageBytes, "image/jpeg");
        }
    }
}
