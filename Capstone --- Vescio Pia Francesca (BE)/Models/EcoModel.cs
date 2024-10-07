using Capstone_____Vescio_Pia_Francesca__BE_.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Models
{
    public class EcoModel : DescribedModifiableModel
    { 
        public int Position { get; set; }
        public IFormFile Photo { get; set; }
        public int? NationId { get; set; }
    }
}
