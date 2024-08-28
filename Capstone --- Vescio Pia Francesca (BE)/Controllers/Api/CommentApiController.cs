using Capstone_____Vescio_Pia_Francesca__BE_.Context;
using Capstone_____Vescio_Pia_Francesca__BE_.DTO;
using Capstone_____Vescio_Pia_Francesca__BE_.Entity;
using Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CommentApiController : ControllerBase
    {
        private readonly ICommentService _commentSvc;
        private readonly DataContext _db;

        public CommentApiController(ICommentService commentSvc, DataContext db)
        {
            _commentSvc = commentSvc;
            _db = db;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentSvc.GetAllComments();
            return Ok(comments);
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
    }
}
