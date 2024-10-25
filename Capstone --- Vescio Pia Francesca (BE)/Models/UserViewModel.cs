using Capstone_____Vescio_Pia_Francesca__BE_.Entity;
using System.ComponentModel.DataAnnotations;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Models
{
    public class UserViewModel : BaseModel
    {

        [Required(ErrorMessage = "Il nome è obbligatorio")]
        [StringLength(30)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Il cognome è obbligatorio")]
        [StringLength(30)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "L'username è obbligatorio")]
        [StringLength(30)]
        public string Username { get; set; }

        [Required(ErrorMessage = "La data di nascita è obbligatoria")]
        public DateTime DateBirth { get; set; }

        [Required(ErrorMessage = "L'e-mail è obbligatoria")]
        [EmailAddress(ErrorMessage = "Inserisci un e-mail valida")]
        public string Email { get; set; }
        [Required]
        [StringLength(30)]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage ="La password deve contenere: almeno una lettera maiuscola, almeno una lettera minuscola, almeno una lettera minuscola, almeno un numero, almeno un carattere speciale ")]
        public string Password { get; set; }

        [StringLength(6)]
        public string? AdminCode { get; set; }
        public List<Role> Roles { get; set; } = [];
    }
}
