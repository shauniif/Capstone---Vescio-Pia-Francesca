using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Models
{
    public class EventModel : DescribedModifiableModel
    {
        public IFormFile Cover { get; set; }
        public DateTime Date { get; set; }
        public int YearOfEvent { get; set; }
        public string Influence { get; set; }
        public bool IsChanged { get; set; }
    }
}
