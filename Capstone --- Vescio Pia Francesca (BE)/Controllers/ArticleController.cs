using Capstone_____Vescio_Pia_Francesca__BE_.Entity;
using Capstone_____Vescio_Pia_Francesca__BE_.Models;
using Capstone_____Vescio_Pia_Francesca__BE_.Services.Classes;
using Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleSvc;

        public ArticleController(IArticleService articleSvc)
        {
            _articleSvc = articleSvc;

        }
        public async Task<IActionResult> GetArticleImage(int id)
        {
            var nation = await _articleSvc.Read(id);
            if (nation?.Image == null)
            {
                return NotFound(); // O un'immagine placeholder
            }
            var nationPhotodata = nation.Image.Substring(23);
            byte[] imageBytes = Convert.FromBase64String(nationPhotodata);
            return File(imageBytes, "image/jpeg");
        }
        public async Task<IActionResult> AllArticles()
        {
            var articles = await _articleSvc.GetAllArticles();
            return View(articles);
        }

        public async Task<IActionResult> MyArticle()
        {
            var name = User.Identity.Name;
            var articles = await _articleSvc.GetAllArticlesOfAdmin(name);
            return View(articles);
        }

        public IActionResult Create() 
        { 
            return View(); 
        }

        [HttpPost]
        public async Task<IActionResult> Create(ArticleModel article, string name)
        {
            if(ModelState.IsValid)
            {
                await _articleSvc.Create(article, name);
                return RedirectToAction(nameof(AllArticles));
            }
            return View(article);
        }

        public async Task<IActionResult> Update(int id)
        {
            var article = await _articleSvc.Read(id);
            var articleModel = new ArticleModel
            {
                Id = article.Id,
                Content = article.Content,
                Title = article.Title,

            };
            return View(articleModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ArticleModel articleModel)
        {
            if (ModelState.IsValid)
            {
                await _articleSvc.Update(articleModel);
                return RedirectToAction(nameof(AllArticles));
            }
            return View(articleModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _articleSvc.Delete(id);
            return RedirectToAction(nameof(AllArticles));
        }
    }
}
