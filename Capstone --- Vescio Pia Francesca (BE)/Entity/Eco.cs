using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Entity
{
    public class Eco : DescribedModifiableEntity
    {

        public int Position { get; set; }
        public string Pic { get; set; }

        public Nation? Nation { get; set; }

        public List<Character> Devotees = new List<Character>();

      

    }
}
