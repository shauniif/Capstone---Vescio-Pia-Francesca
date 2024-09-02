using Capstone_____Vescio_Pia_Francesca__BE_.Entity;
using System.ComponentModel.DataAnnotations;

namespace Capstone_____Vescio_Pia_Francesca__BE_.DTO
{
    public class CommentDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Il commento può contenere massimo 200 caratteri.")]
        public required string Content { get; set; }

        [Required]
        public required int AuthorId { get; set; }

        [Required]
        public required int ArticleId { get; set; }
        
        
}
}
