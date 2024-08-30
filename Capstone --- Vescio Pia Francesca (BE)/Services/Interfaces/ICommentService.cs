using Capstone_____Vescio_Pia_Francesca__BE_.Entity;
using Capstone_____Vescio_Pia_Francesca__BE_.DTO;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces
{
    public interface ICommentService
    {
        Task<IEnumerable<Comment>> GetAllComments();
        Task<IEnumerable<Comment>> GetAllCommentsOfArticle(int id);
        Task<Comment> Create(CommentDTO dto);

        Task<Comment> Update(CommentDTO dto);

        Task<Comment> Delete(int id);
        Task<Comment> Read(int id);

        Task<Comment> SelectComment(int id);
    }
}
