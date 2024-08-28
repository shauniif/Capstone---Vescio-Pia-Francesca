using Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityApiController : ControllerBase
    {
        private readonly ICitiesService _citiesSvc;
        public CityApiController(ICitiesService citiesSvc, INationService nationSvc)
        {
            _citiesSvc = citiesSvc;
        }

        [HttpGet]
        public async Task<IActionResult> AllCities()
        {
            var cities = await _citiesSvc.GetAllCities();
            return Ok(cities);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var city = await _citiesSvc.Read(id);
            return Ok(city);
        }


    }
}
