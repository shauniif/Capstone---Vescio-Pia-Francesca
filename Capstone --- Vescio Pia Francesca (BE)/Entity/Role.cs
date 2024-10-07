using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Entity
{
    public class Role : BaseEntity
    {

        [Required]
        [StringLength(50)]
        public required string Name { get; set; }

        public List<User> Users { get; set; } = new List<User>();
    }
}
