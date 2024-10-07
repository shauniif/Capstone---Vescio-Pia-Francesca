using System.ComponentModel.DataAnnotations.Schema;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Entity
{
    public class ModifiableEntity : NamedEntity
    {
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Modifier { get; set; }
    }
}
