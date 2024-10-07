using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Entity
{
    public class Race : ModifiableEntity
    {
        public List<Character> Character { get; set; } = new List<Character>();
    }
}
