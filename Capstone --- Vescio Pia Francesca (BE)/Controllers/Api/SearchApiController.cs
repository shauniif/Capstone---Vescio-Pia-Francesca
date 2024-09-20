using Capstone_____Vescio_Pia_Francesca__BE_.DTO;
using Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SearchApiController : ControllerBase
    {
        private readonly IEcoService _ecoSvc;
        private readonly IGuildService _guildSvc;
        private readonly ICitiesService _citySvc;
        private readonly INationService _nationSvc;
        public SearchApiController(IEcoService ecoSvc, IGuildService guildSvc, ICitiesService citySvc, INationService nationSvc)
        {
            _ecoSvc = ecoSvc;
            _guildSvc = guildSvc;
            _citySvc = citySvc;
            _nationSvc = nationSvc;
        }


        [HttpGet("{search}")]
        public async Task<IActionResult> Index(string search)
        {
            var ecos = await _ecoSvc.Search(search);
            var guilds = await _guildSvc.Search(search);
            var cities = await _citySvc.Search(search);
             var nations = await _nationSvc.Search(search);
            return Ok(new SearchResponseDTO
            {
                Ecos = ecos.ToList(),
                Guilds = guilds.ToList(),
                Cities = cities.ToList(),
                Nations = nations.ToList()
            });
        }
    }
}
