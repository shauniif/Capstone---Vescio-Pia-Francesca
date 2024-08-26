using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Entity
{
    public class Race
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Modifier { get; set; }

        public List<Character> Character { get; set; } = new List<Character>();
    }
}
