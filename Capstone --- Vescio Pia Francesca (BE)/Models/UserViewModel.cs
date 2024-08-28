using Capstone_____Vescio_Pia_Francesca__BE_.Entity;
using System.ComponentModel.DataAnnotations;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Models
{
    public class UserViewModel
    {

        [Required(ErrorMessage = "Il nome è obbligatorio")]
        [StringLength(30)]
        public required string Name { get; set; }

        [Required]
        [StringLength(30)]
        public  string Username { get; set; }

        [Required]
        public required DateTime DateBirth { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        [Required]
        [StringLength(30)]
        public string Password { get; set; }

        [StringLength(6)]
        public string? AdminCode { get; set; }
        public List<Role> Roles { get; set; } = [];
    }
}
