using Capstone_____Vescio_Pia_Francesca__BE_.DTO;
using Capstone_____Vescio_Pia_Francesca__BE_.Entity;
using Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuildRoleApiController : ControllerBase
    {
        private readonly IGuildRoleService _guildRoleSvc;

        public GuildRoleApiController(IGuildRoleService guildRoleSvc)
        {
            _guildRoleSvc = guildRoleSvc;

        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromForm] GuildRoleDTO dto)
        {
            if (ModelState.IsValid)
            {
                var guildRole = await _guildRoleSvc.Create(dto);
                
                return Ok(guildRole);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            await _guildRoleSvc.Delete(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] GuildRoleDTO dto)
        {
            if (ModelState.IsValid)
            {
                dto.Id = id;
                var guildRole = await _guildRoleSvc.Update(dto);
                return Ok(guildRole);
            }
            return BadRequest();

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var guildRole = await _guildRoleSvc.Read(id);
            var guildRoleSel = await _guildRoleSvc.Read(guildRole.Id);
            return Ok(guildRoleSel);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var guildRoles = await _guildRoleSvc.GetAll();
            
            return Ok(guildRoles);
        }

        
    }
}
