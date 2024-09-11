using System.ComponentModel.DataAnnotations;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Models
{
    public class LoginAdminModel
    {
        [Display(Name = "Codice Admin")]
        [Required(ErrorMessage = "Il codice identificativo dell'admin è obbligatorio")]
        [StringLength(6, MinimumLength = 1, ErrorMessage = "Il codice identificativo dell'admin deve essere lungo 6 caratteri")]
        public required string AdminCode { get; set; }
        [Required]
        [StringLength(30)]
        public string Password { get; set; }
    }
}
