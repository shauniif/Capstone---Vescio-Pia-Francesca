using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Entity
{
    public class User : BaseEntity
    {        

        [Required]
        [StringLength(30)]
        public required string FirstName { get; set; }

        [Required]
        [StringLength(30)]
        public required string LastName { get; set; }

        [Required]
        [StringLength(30)]
        public required string Username { get; set; }

        [Required]
        public required DateTime DateBirth { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Tipo di email non valido")]
        public required string Email { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string? Password { get; set; }

        [Column(TypeName = "nvarchar(6)")]
        public string? AdminCode { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string? Image { get; set; }
        public List<Role> Roles { get; set; } = new List<Role>();

        public List<Article> Articles { get; set; } = new List<Article>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public List<Character> Characters { get; set; } = new List<Character>();
    }
}
