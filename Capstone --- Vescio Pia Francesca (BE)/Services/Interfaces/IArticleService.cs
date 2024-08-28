using Capstone_____Vescio_Pia_Francesca__BE_.Entity;
using Capstone_____Vescio_Pia_Francesca__BE_.Models;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces
{
    public interface IArticleService
    {
        Task<Article> Create(ArticleModel model, string name);
        Task<Article> Update(ArticleModel model);
        Task<Article> Delete(int id);
        Task<Article> Read(int id);
        Task<IEnumerable<Article>> GetAllArticles();
        Task<IEnumerable<Article>> GetAllArticlesOfAdmin(string name);
    }
}
