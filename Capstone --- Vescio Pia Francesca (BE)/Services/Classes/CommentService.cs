using Capstone_____Vescio_Pia_Francesca__BE_.Context;
using Capstone_____Vescio_Pia_Francesca__BE_.DTO;
using Capstone_____Vescio_Pia_Francesca__BE_.Entity;
using Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Services.Classes
{
    public class CommentService : ICommentService
    {
        private readonly DataContext _db;

        public CommentService(DataContext db)
        {
            _db = db;
        }
        public async Task<Comment> Create(CommentDTO dto)
        { try
            {
                var article = await _db.Articles.SingleOrDefaultAsync(a => a.Id == dto.ArticleId);
                if (article == null)
                {
                    throw new Exception("Article not found");
                };
                var author = await _db.Users.SingleOrDefaultAsync(a => a.Id == dto.AuthorId);
                if (author == null)
                {
                    throw new Exception("Author not found");
                };

                var comment = new Comment
                {
                    Article = article,
                    Author = author,
                    Content = dto.Content,
                    PublicationDate = DateTime.Now
                };
                 await _db.Comments.AddAsync(comment);
                await _db.SaveChangesAsync();
                return comment;
            }
            catch (Exception ex) 
            {
                throw new Exception("Create failed", ex);
            }
           
        }

        public async Task<Comment> Delete(int id)
        {
            try
            {
                var comment = await Read(id);
                _db.Comments.Remove(comment);
                await _db.SaveChangesAsync();
                return comment;
            }
            catch (Exception ex) 
            {
                throw new Exception("Delete failed", ex);
            }
        }

        public async Task<IEnumerable<Comment>> GetAllComments()
        {
            try
            {
                var comments = await _db.Comments
                    .Include(c => c.Article)
                    .ToListAsync();
                return comments;
            }
            catch (Exception ex) 
            {
                throw new Exception("Comments not found", ex);
            }
        }

        public async Task<IEnumerable<Comment>> GetAllCommentsOfArticle(int id)
        {
            try
            {
                var comments = await _db.Comments
                    .Include(c => c.Article)
                    .Where(Article => Article.Id == id)
                    .ToListAsync();
                return comments;
            }
            catch (Exception ex)
            {
                throw new Exception("Comments not found", ex);
            }
        }

        public async Task<Comment> Read(int id)
        {
            try 
            {
                var comment = await SelectComment(id);
                if (comment == null)
                {
                    throw new Exception("comment not found");
                }
                return comment;
                
            } catch (Exception ex) 
            {
                throw new Exception("Error", ex);
            }
        }
        public async Task<Comment> Update(CommentDTO dto)
        {
            try
            {
                var currComment = await _db.Comments.FirstOrDefaultAsync(c=> c.Id == dto.Id);
                if (currComment == null)
                {
                    throw new Exception("Comment not found");
                }

                currComment.Content = dto.Content;
                await _db.SaveChangesAsync();

                return await SelectComment(currComment.Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Update failed", ex);
            }

        }
        
        public async Task<Comment> SelectComment(int id)
        {
            try
            {
                var comment = await _db.Comments
                    .AsNoTracking()
                    .Include(c => c.Article)
                    .Include(c => c.Author)
                    .Where(c => c.Id == id)
                    .Select(c => new Comment
                        {
                            Id = c.Id,
                            Content = c.Content,
                            PublicationDate = c.PublicationDate,
                            Article = new Article
                        {
                            Id = c.Article.Id, 
                            Image = c.Article.Image,
                            Title = c.Article.Title,
                            Content = c.Article.Content
                        },
                            Author = new User
                        {
                            Id = c.Author.Id, 
                            Name = c.Author.Name,
                            Email = c.Author.Email,
                            DateBirth = c.Author.DateBirth,
                            Username = c.Author.Username
                        }
                    })
                    .FirstOrDefaultAsync();
                if (comment == null)
                {
                    throw new Exception("Comment not found");
                }
                return comment;
            }
            catch (Exception ex) 
            {
                throw new Exception("Comment not found", ex);
            }
        }
    }
}
