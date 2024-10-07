using Capstone_____Vescio_Pia_Francesca__BE_.Entity;
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
            var cities = await _citiesSvc.GetAll();
            var citiesSel = new List<City>();
            foreach (var city in cities) 
            {
                var citySel = await _citiesSvc.GetCity(city.Id);
                citiesSel.Add(citySel);
            }
            return Ok(citiesSel);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var city = await _citiesSvc.Read(id);
            var citySel = await _citiesSvc.GetCity(city.Id);
            return Ok(citySel);
        }


    }
}
