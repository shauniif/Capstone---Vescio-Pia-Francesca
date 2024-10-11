using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Entity
{
    public class Comment : BaseEntity
    {

        [Required]
        [StringLength(1024)]
        public required string Content { get; set; }

        [Required]
        public required User Author { get; set; }

        [Required]
        public Article Article { get; set; }

        public DateTime PublicationDate { get; set; } = DateTime.Now;
    }
}