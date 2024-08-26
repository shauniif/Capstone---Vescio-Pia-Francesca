using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Entity
{
    public class Eco
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int Position { get; set; }

        [MaxLength(80)]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Photo { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Modifier { get; set; }
        public Nation? Nation { get; set; }

        public List<Character> Devotees = new List<Character>();

    }
}
