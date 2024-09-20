using Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces;
using Capstone_____Vescio_Pia_Francesca__BE_.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
            var guildsSelect = new List<Guild>();
            foreach (var guild in guilds)
            {
                var guildSelect = await _guildSvc.GetGuild(guild.Id);
                guildsSelect.Add(guildSelect);
            }
            return Ok(guildsSelect);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var guild = await _guildSvc.Read(id);
            var guildSelect = await _guildSvc.GetGuild(guild.Id);
            return Ok(guildSelect);
        }
    }
}
