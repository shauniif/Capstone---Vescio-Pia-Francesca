using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Entity
{
    public class Article : BaseEntity
    {
        [MinLength(5)]
        [MaxLength(50)]
        public required string Title { get; set; }
        
        [Column(TypeName = "nvarchar(max)")]
        public required string Image {  get; set; }
        public required string Content { get; set; }
        public User? Author { get; set; }
        public DateTime PublicationDate { get; set; }

        public List<Comment> Comments { get; set; } = new List<Comment>();

        public string ShortContent
        {
            get
            {
                if (string.IsNullOrEmpty(Content))
                {
                    return string.Empty;
                }

                if (Content.Length > 75)
                {
                    return Content.Substring(0, 75) + "...";
                }

                return Content;
            }
        }
    }
}
