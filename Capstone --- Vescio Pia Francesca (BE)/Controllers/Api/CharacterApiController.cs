using Capstone_____Vescio_Pia_Francesca__BE_.DTO;
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
    public class CharacterApiController : ControllerBase
    {
        private readonly ICharacterService _characterSvc;

        public CharacterApiController(ICharacterService characterSvc)
        {
            _characterSvc = characterSvc;

        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromForm] CharacterDTO dto)
        {
            if (ModelState.IsValid)
            {
                var character = await _characterSvc.Create(dto);
                var characterSel = await _characterSvc.Read(character.Id);
                return Ok(characterSel);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            await _characterSvc.Delete(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] CharacterDTO dto)
        {
            if (ModelState.IsValid)
            {
                dto.Id = id;
                var character = await _characterSvc.Update(dto);
                return Ok(character);
            }
            return BadRequest();

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var character = await _characterSvc.Read(id);
            var characterSel = await _characterSvc.Read(character.Id);
            return Ok(characterSel);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var characters = await _characterSvc.GetAll();
            var charactersSel = new List<Character>();
            foreach (var character in characters) {
                var characterSel = await _characterSvc.Read(character.Id);
                charactersSel.Add(characterSel);
            }
            return Ok(charactersSel);
        }

        [HttpGet("OfUser/{id}")]
        public async Task<IActionResult> GetAllOfUSer(int id)
        {
            var characters = await _characterSvc.GetAllByUser(id);
            var charactersSel = new List<Character>();
            foreach (var character in characters)
            {
                var characterSel = await _characterSvc.Read(character.Id);
                charactersSel.Add(characterSel);
            }
            return Ok(charactersSel);
        }

        [HttpPatch("{id},{idRole}")]
        public async Task<IActionResult> AddOrRemoveRole(int id, int idRole)
        {

            await _characterSvc.AddOrRemoveRole(id, idRole);
                return Ok();
        }
    }
   
}
