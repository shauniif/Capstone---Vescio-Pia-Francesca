using Capstone_____Vescio_Pia_Francesca__BE_.Entity;
using Capstone_____Vescio_Pia_Francesca__BE_.DTO;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces
{
    public interface ICommentService : CrudService<Comment, CommentDTO>
    {
        Task<IEnumerable<Comment>> GetAllCommentsOfArticle(int id);
        Task<Comment> SelectComment(int id);
    }
}
