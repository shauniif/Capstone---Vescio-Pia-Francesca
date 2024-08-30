using Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class RaceApiController : ControllerBase
    {
        private readonly IRacesService _raceSvc;

        public RaceApiController(IRacesService raceSvc)
        {
            _raceSvc = raceSvc;

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var races = await _raceSvc.GetAllRaces();
            return Ok(races);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var race = await _raceSvc.Read(id);
            return Ok(race);
        }
    }

}
