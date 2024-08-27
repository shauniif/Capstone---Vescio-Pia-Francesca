using Capstone_____Vescio_Pia_Francesca__BE_.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Models
{
    public class GuildModel
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }

        public int NationId { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Modifier { get; set; }

        public List<Character> Character { get; set; } = new List<Character>();
    }
}
