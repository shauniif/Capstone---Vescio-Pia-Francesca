using Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleApiController : ControllerBase
    {
        private readonly IArticleService _articleSvc;

        public ArticleApiController(IArticleService articleSvc)
        {
            _articleSvc = articleSvc;

        }

        [HttpGet]
        public async Task<IActionResult> AllArticles()
        {
            var article = await _articleSvc.GetAllArticles();
            return Ok(article);
        }

        [HttpGet("{id}")]
        public async Task <IActionResult> Get(int id)
        {
            var article = await _articleSvc.Read(id);
            return Ok(article);
        }
    }
}
