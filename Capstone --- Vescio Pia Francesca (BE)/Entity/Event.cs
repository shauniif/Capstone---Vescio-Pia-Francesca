using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Entity
{
    public class Event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(150)]
        public string Name { get; set; }
        public string Cover { get; set; }
        public DateTime Date { get; set; }

        public int YearOfEvent { get; set; }

        public string Description { get; set; }

        public List<string> Influence { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Modifier { get; set; }
    }
}
