using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Entity
{
    public class Character : BaseEntity
    {

        [MaxLength(50)]
        public string Name { get; set; }

        public string Image { get; set; }

        public Guild? Guild { get; set; }

        public City City { get; set; }

        public Race Race { get; set; }

        public Eco? Eco { get; set; }

        public User User { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Score { get; set; }

        public string Background { get; set; }

        public GuildRole? GuildRole { get; set; }
    }
}
