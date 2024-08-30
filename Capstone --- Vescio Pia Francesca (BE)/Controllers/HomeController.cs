using Capstone_____Vescio_Pia_Francesca__BE_.Models;
using Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Capstone_____Vescio_Pia_Francesca__BE_.Context;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Controllers
{
    public class HomeController : Controller
    {
        private static DateTime? _lastCheckDate = null;
        private readonly ILogger<HomeController> _logger;
        private readonly IEcoService _ecoSvc;
        private readonly IGuildService _guildSvc;
        private readonly INationService _nationSvc;
        private readonly IRacesService _racesSvc;
        private readonly IEventService _eventSvc;
        private readonly ICharacterService _characterSvc;
        private readonly DataContext _db;

        public HomeController(ILogger<HomeController> logger, IEcoService ecoSvc, IGuildService guildSvc, INationService nationSvc, IRacesService raceSvc, IEventService eventSvc, ICharacterService characterSvc, DataContext db)
        {
            _logger = logger;
            _ecoSvc = ecoSvc;
            _guildSvc = guildSvc;
            _nationSvc = nationSvc;
            _racesSvc = raceSvc;
            _eventSvc = eventSvc;
            _characterSvc = characterSvc;
            _db = db;
        }

        
        public async Task<IActionResult> Index()
        {

           
                var ecos = await _ecoSvc.GetAllEcos();
                var guilds = await _guildSvc.GetAll();
                var nations = await _nationSvc.GetAllNations();
                var races = await _racesSvc.GetAllRaces();
                 var characters = await _characterSvc.GetAll();
                foreach (var nation in nations)
                {
                    nation.Modifier = await _eventSvc.ChangeModifier(nation.Name, nation.Modifier);
                }
                foreach (var eco in ecos)
                {
                    eco.Modifier = await _eventSvc.ChangeModifier(eco.Name, eco.Modifier);
                }
                foreach (var guild in guilds)
                {
                    guild.Modifier = await _eventSvc.ChangeModifier(guild.Name, guild.Modifier);
                }
                foreach (var race in races)
                {
                    race.Modifier = await _eventSvc.ChangeModifier(race.Name, race.Modifier);
                }

            foreach (var character in characters)
            {
                Console.WriteLine($"{character.Id}:{character.Score} prima:" );

            };
            foreach (var character in characters)
                {
                    character.Score = await _characterSvc.ChangeScore(character.Id);
                    Console.WriteLine($"{character.Id}:{character.Score} durante:");
            };

            foreach (var character in characters)
            {
                Console.WriteLine($"{character.Id}:{character.Score} dopo:");

            }
            await _db.SaveChangesAsync();
            foreach (var character in characters)
            {
                Console.WriteLine($"{character.Id}:{character.Score} dopo salvataggio:");

            }
            return View();
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
