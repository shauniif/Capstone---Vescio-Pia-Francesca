using Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces;
using Capstone_____Vescio_Pia_Francesca__BE_.Entity;
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
    }
}
