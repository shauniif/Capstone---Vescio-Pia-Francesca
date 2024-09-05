using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Capstone_____Vescio_Pia_Francesca__BE_.DTO
{
    public class RaceDTO
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Modifier { get; set; }
    }
}
