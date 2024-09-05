using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Capstone_____Vescio_Pia_Francesca__BE_.DTO
{
    public class UserResponseDTO
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

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
        public string? Image { get; set; }

    }
}
