using Capstone_____Vescio_Pia_Francesca__BE_.Models;
using Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Capstone_____Vescio_Pia_Francesca__BE_.Context;
using Microsoft.AspNetCore.Authentication.Cookies;
using Capstone_____Vescio_Pia_Francesca__BE_.Views;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IEcoService _ecoSvc;
        private readonly IGuildService _guildSvc;
        private readonly INationService _nationSvc;
        private readonly IRacesService _racesSvc;
        private readonly IEventService _eventSvc;
        private readonly ICharacterService _characterSvc;
        private readonly IAuthService _authSvc;
        private readonly DataContext _db;

        public HomeController(ILogger<HomeController> logger, IEcoService ecoSvc, IGuildService guildSvc, INationService nationSvc, IRacesService raceSvc, IEventService eventSvc, ICharacterService characterSvc, DataContext db, IAuthService authSvc)
        {
            _logger = logger;
            _ecoSvc = ecoSvc;
            _guildSvc = guildSvc;
            _nationSvc = nationSvc;
            _racesSvc = raceSvc;
            _eventSvc = eventSvc;
            _characterSvc = characterSvc;
            _db = db;
            _authSvc = authSvc;
        }

        
        public async Task<IActionResult> Index()
        {

           
                var ecos = await _ecoSvc.GetAll();
                var guilds = await _guildSvc.GetAll();
                var nations = await _nationSvc.GetAll();
                var races = await _racesSvc.GetAll();
                var characters = await _characterSvc.GetAll();
   
                await _eventSvc.CalcuateModifier(ecos, guilds, nations, races, characters);


            return View();
        }



        
        public IActionResult Privacy()
        {
            return View();
        }

    
        public IActionResult Error()
        {
            return View();
        }

        [Route("/StatusCodeError/{statusCode}")]
        public IActionResult Error(int statusCode)
        {
            if (statusCode == 404)
            {
                ViewBag.ErrorMessage = "Errore";
            }
            return View();
        }

        public async Task<IActionResult> GetUserImage(int id)
        {
            var user = await _authSvc.Read(id);
            if (user?.Image == null)
            {
                return NotFound();
            }
            var userImgData = user.Image.Substring(23);
            byte[] imageBytes = Convert.FromBase64String(userImgData);
            return File(imageBytes, "image/jpeg");
        }
    }
}
