using Capstone_____Vescio_Pia_Francesca__BE_.Entity;
using Capstone_____Vescio_Pia_Francesca__BE_.Models;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces
{
    public interface IArticleService : CrudService<Article, ArticleModel>
    {
        Task<Article> GetArticle(int id);
        Task<IEnumerable<Article>> GetAllArticlesOfAdmin(string name);
        

    }
}
