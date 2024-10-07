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
    public class EcoApiController : ControllerBase
    {
        private readonly IEcoService _ecoSvc;
        public EcoApiController(IEcoService ecoSvc)
        {
            _ecoSvc = ecoSvc;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var ecos = await _ecoSvc.GetAll();
            var ecosSelect = new List<Eco>();
            foreach (var nation in ecos)
            {
                var nationSel = await _ecoSvc.GetEco(nation.Id);
                ecosSelect.Add(nationSel);
            }
            return Ok(ecosSelect);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var nation = await _ecoSvc.Read(id);
            var nationSel = await _ecoSvc.GetEco(nation.Id);
            return Ok(nationSel);
        }
    }
}

