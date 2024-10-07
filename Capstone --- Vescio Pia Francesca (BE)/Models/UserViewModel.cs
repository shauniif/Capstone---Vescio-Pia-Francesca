using Capstone_____Vescio_Pia_Francesca__BE_.Entity;
using System.ComponentModel.DataAnnotations;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Models
{
    public class UserViewModel : BaseModel
    {

        [Required(ErrorMessage = "Il nome è obbligatorio")]
        [StringLength(30)]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "Il nome è obbligatorio")]
        [StringLength(30)]
        public required string LastName { get; set; }

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
       // [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage ="La password deve contenere: almeno una lettera maiuscola, almeno una lettera minuscola, almeno una lettera minuscola, almeno un numero, almeno un carattere speciale ")]
        public string Password { get; set; }

        [StringLength(6)]
        public string? AdminCode { get; set; }
        public List<Role> Roles { get; set; } = [];
    }
}
