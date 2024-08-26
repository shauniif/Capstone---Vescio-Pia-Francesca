using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Entity
{
    public class City
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        public string Description { get; set; }

        public Nation Nation { get; set; }

        public List<Character> Character { get; set; } = new List<Character>();
    }
}
