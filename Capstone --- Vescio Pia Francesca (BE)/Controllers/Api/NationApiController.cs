using Capstone_____Vescio_Pia_Francesca__BE_.Entity;
using Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Capstone_____Vescio_Pia_Francesca__BE_.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class NationApiController : ControllerBase
    {
        private readonly INationService _nationSvc;

        public NationApiController(INationService nationSvc)
        {
            _nationSvc = nationSvc;

        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        { var nations = await _nationSvc.GetAllNations();
            return Ok(nations);
        }

        // GET api/<NationApiController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAll(int id)
        {
            var nation = await _nationSvc.Read(id);
            return Ok(nation);
        }

    }
}
