using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Models
{
    public class EventModel
    {
        public int Id { get; set; }

        [MaxLength(150)]
        public string Name { get; set; }
        public IFormFile Cover { get; set; }
        public DateTime Date { get; set; }

        public int YearOfEvent { get; set; }

        public string Description { get; set; }

        public string Influence { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Modifier { get; set; }
    }
}
