using Capstone_____Vescio_Pia_Francesca__BE_.Context;
using Capstone_____Vescio_Pia_Francesca__BE_.Entity;
using Capstone_____Vescio_Pia_Francesca__BE_.Models;
using Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Services.Classes
{
    public class ArticleService : IArticleService
    {
        private readonly DataContext _db;

        public ArticleService(DataContext db)
        {
            _db = db;
        }
        private string ConvertImage(IFormFile image)
        {

            using (MemoryStream memoryStream = new MemoryStream())
            {
                image.CopyTo(memoryStream);
                byte[] fileBytes = memoryStream.ToArray();
                string base64String = Convert.ToBase64String(fileBytes);
                string urlImg = $"data:image/jpeg;base64,{base64String}";
                return urlImg;
            }

        }
        public async Task<Article> Create(ArticleModel model, string name)
        {   try
            {
                var author = await _db.Users.FirstOrDefaultAsync(u => u.Name == name);
                var article = new Article
                {
                    Title = model.Title,
                    Content = model.Content,
                    PublicationDate = DateTime.Now,
                    Image = ConvertImage(model.Image),
                    Author = author
                };
                await _db.Articles.AddAsync(article);
                await _db.SaveChangesAsync();
                return article;
            }
            catch (Exception ex) 
            {
                throw new Exception("Create failed", ex);
            }
           
        }

        public async Task<Article> Delete(int id)
        { try
            {
                var article = await Read(id);
                _db.Articles.Remove(article);
                await _db.SaveChangesAsync();
                return article;
            }
            catch (Exception ex) 
            {
                throw new Exception("Delete failed", ex);
            }
            
        }

        public async Task<IEnumerable<Article>> GetAllArticles()
        {
            try
            {
                var articles = await _db.Articles.Include(a => a.Author).ToListAsync();
                return articles;
            }
            catch (Exception ex) 
            {
                throw new Exception("Articles not found", ex);
            }
        }

        public async Task<IEnumerable<Article>> GetAllArticlesOfAdmin(string name)
        {
            try
            {
                var articles = await _db.Articles
                    .Include(a => a.Author)
                    .Where(a => a.Author.Name == name)
                    .ToListAsync();
                return articles;
            }
            catch (Exception ex)
            {
                throw new Exception("Articles not found", ex);
            }
        }

        public async Task<Article> Read(int id)
        {
            try
            {
                var article = await _db.Articles.Include(a => a.Author).FirstOrDefaultAsync(a => a.Id == id);
                if(article == null)
                {
                    throw new Exception("Article not found");
                }
                return article;
            }
            catch (Exception ex) 
            {
                throw new Exception("Error", ex);
            }
        }

        public async Task<Article> Update(ArticleModel model)
        { try
            {
                var article = await Read(model.Id);
                article.Title = model.Title;
                article.Content = model.Content;
                article.Image = article.Image;

                _db.Articles.Update(article);
                await _db.SaveChangesAsync();
                return article;
            }
            catch (Exception ex) 
            {
                throw new Exception("Update failed", ex);
            }
        }

        public async Task<Article> GetArticle(int id)
        {
            var article = await _db.Articles
                      .Include(a => a.Author)
                      .Select(a => new Article
                      {
                         Id = a.Id,
                         Image = a.Image,
                         Title = a.Title,
                         PublicationDate = a.PublicationDate,
                         Content = a.Content,
                         Author = new User { 
                             Id = a.Author.Id,
                             Name = a.Author.Name,
                             DateBirth = a.Author.DateBirth,
                             Email = a.Author.Email,
                             Username = a.Author.Username
                         },
                         Comments = a.Comments.Select(c => new Comment
                         {
                             Id=c.Id,
                             Content=c.Content,
                             Author = new User
                             {
                                 Id = c.Author.Id,
                                 Name = c.Author.Name,
                                 DateBirth = c.Author.DateBirth,
                                 Email=c.Author.Email,
                                 Username = c.Author.Username
                             },
                             PublicationDate = c.PublicationDate,
                             Article = new Article
                             {
                                 Id = c.Article.Id,
                                 Title = c.Article.Title,
                                 Content = c.Article.Content,
                                 Author = new User
                                 {
                                     Id = a.Author.Id,
                                     Name = a.Author.Name,
                                     DateBirth = a.Author.DateBirth,
                                     Email = a.Author.Email,
                                     Username = a.Author.Username
                                 },
                                 Image = c.Article.Image
                             }
                         }).ToList()
                      }).FirstOrDefaultAsync(c => c.Id == id);
            if (article == null)
            {
                throw new Exception();
            }
            return article;
        }
    }
}
