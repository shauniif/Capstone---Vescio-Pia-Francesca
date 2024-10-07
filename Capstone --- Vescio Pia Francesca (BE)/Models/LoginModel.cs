using System.ComponentModel.DataAnnotations;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Models
{
    public class LoginModel
    {
        
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        [Required]
        [StringLength(30)]
        public string Password { get; set; }
    }
}
