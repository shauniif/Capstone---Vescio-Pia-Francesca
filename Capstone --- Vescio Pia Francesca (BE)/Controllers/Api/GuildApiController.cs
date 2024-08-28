using Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuildApiController : ControllerBase
    {
        private readonly IGuildService _guildSvc;
        public GuildApiController(IGuildService guildSvc, INationService nationSvc)
        {
            _guildSvc = guildSvc;
        }
        [HttpGet]
        public async Task<IActionResult> GeAll()
        {
            var guilds = await _guildSvc.GetAll();
            return Ok(guilds);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var guild = await _guildSvc.Read(id);
            return Ok(guild);
        }
    }
}
