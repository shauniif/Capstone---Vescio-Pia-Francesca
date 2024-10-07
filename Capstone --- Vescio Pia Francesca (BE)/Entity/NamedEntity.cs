using System.ComponentModel.DataAnnotations;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Entity
{
    public class NamedEntity : BaseEntity
    {
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
