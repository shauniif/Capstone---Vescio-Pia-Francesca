using Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces;
using Capstone_____Vescio_Pia_Francesca__BE_.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ArticleApiController : ControllerBase
    {
        private readonly IArticleService _articleSvc;
        private readonly IAuthService _authSvc;

        public ArticleApiController(IArticleService articleSvc, IAuthService authSvc)
        {
            _articleSvc = articleSvc;
            _authSvc = authSvc;
        }

        [HttpGet]
                [AllowAnonymous]
        public async Task<IActionResult> AllArticles()
        {
            var articles = await _articleSvc.GetAllArticles();
            var articlesSelet = new List<Article>();
            foreach (var article in articles) 
            {
                var articleSel = await _articleSvc.GetArticle(article.Id);
                articlesSelet.Add(articleSel);
            }
            return Ok(articlesSelet);
        }

        [HttpGet("{id}")]
        public async Task <IActionResult> Get(int id)
        {
            var article = await _articleSvc.Read(id);
            var articleSel = await _articleSvc.GetArticle(article.Id);
            return Ok(articleSel);
        }

        [HttpGet("author/{id}")]

        public async Task<IActionResult> GetAuthor(int id)
        {
            var author = await _authSvc.GetById(id);
            var authorSel = await _authSvc.CreateUser(author.Id);
            return Ok(authorSel);
        }
    }
}
