using Capstone_____Vescio_Pia_Francesca__BE_.Entity;
using System.ComponentModel.DataAnnotations;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Models
{
    public class ArticleModel
    {
        public int Id { get; set; }
        [MinLength(5)]
        [MaxLength(50)]
        public required string Title { get; set; }
        public IFormFile Image { get; set; }
        public required string Content { get; set; }
        public User? Author { get; set; }
        public DateTime PublicationDate { get; set; }
    }
}
