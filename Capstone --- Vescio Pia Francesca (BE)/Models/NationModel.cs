using Capstone_____Vescio_Pia_Francesca__BE_.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Models
{
    public class NationModel : DescribedModifiableModel
    {
        public string FormOfGovernment { get; set; }
        public IFormFile Photo { get; set; }
        
    }
}
