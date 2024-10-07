using Capstone_____Vescio_Pia_Francesca__BE_.Context;
using Capstone_____Vescio_Pia_Francesca__BE_.DTO;
using Capstone_____Vescio_Pia_Francesca__BE_.Entity;
using Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CommentApiController : ControllerBase
    {
        private readonly ICommentService _commentSvc;

        public CommentApiController(ICommentService commentSvc)
        {
            _commentSvc = commentSvc;

        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentSvc.GetAll();
            return Ok(comments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {   
            var comment = await _commentSvc.Read(id);
            return Ok(comment);
        }


        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CommentDTO dto)
        {
            
            if (ModelState.IsValid)
            {
                
                var comment = await _commentSvc.Create(dto);
                var newComment = await _commentSvc.Read(comment.Id);
                return Ok(newComment);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _commentSvc.Delete(id);
            return NoContent();

        }

        [HttpPut("{id}")] 
        public async Task<IActionResult> Update(int id, [FromBody] CommentDTO dto)
        {
            if(ModelState.IsValid)
            {
                dto.Id = id;
                var comment = await _commentSvc.Update(dto);
                return Ok(comment);
            } 
            return BadRequest();

        }
    }
}
