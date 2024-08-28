using Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class EchoApiController : ControllerBase
    {
        private readonly IEcoService _ecoSvc;
        public EchoApiController(IEcoService ecoSvc)
        {
            _ecoSvc = ecoSvc;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var ecos = await _ecoSvc.GetAllEcos();
            return Ok(ecos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var nation = await _ecoSvc.Read(id);
            return Ok(nation);
        }
    }
}

