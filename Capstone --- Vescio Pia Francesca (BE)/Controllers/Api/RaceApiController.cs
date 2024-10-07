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
            var races = await _raceSvc.GetAll();
            var racesDTO = new List<RaceDTO>();


            foreach (var race in races)
            {
                var raceDTO = await _raceSvc.ReturnRace(race.Id);
                racesDTO.Add(raceDTO);    
            }

            return Ok(racesDTO);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var race = await _raceSvc.Read(id);
            var raceDTO = await _raceSvc.ReturnRace(race.Id);
            return Ok(raceDTO);
        }
    }

}
