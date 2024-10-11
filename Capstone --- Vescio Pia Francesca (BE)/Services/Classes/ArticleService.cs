using Capstone_____Vescio_Pia_Francesca__BE_.Context;
using Capstone_____Vescio_Pia_Francesca__BE_.Entity;
using Capstone_____Vescio_Pia_Francesca__BE_.Models;
using Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Services.Classes
{
    public class ArticleService : ImageService, IArticleService
    {
        private readonly DataContext _db;

        public ArticleService(DataContext db)
        {
            _db = db;
        }

        public async Task<Article> Create(ArticleModel model)
        {   try
            {
                var article = new Article
                {
                    Title = model.Title,
                    Content = model.Content,
                    PublicationDate = DateTime.Now,
                    Image = ConvertImage(model.Image),
                    Author = model.Author,
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

        public async Task<IEnumerable<Article>> GetAll()
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
                    .Where(a => a.Author.FirstName == name)
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
                var article = await _db.Articles
                    .Include(a => a.Author)
                    .Include(a => a.Comments)
                    .ThenInclude(c => c.Author)
                    .FirstOrDefaultAsync(a => a.Id == id);

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
                article.Image = ConvertImage(model.Image);

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
                             FirstName = a.Author.FirstName,
                             LastName = a.Author.LastName,
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
                                 FirstName=c.Author.FirstName,
                                 LastName=c.Author.LastName,
                                 DateBirth = c.Author.DateBirth,
                                 Email=c.Author.Email,
                                 Username = c.Author.Username
                             },
                             PublicationDate = c.PublicationDate, 
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
