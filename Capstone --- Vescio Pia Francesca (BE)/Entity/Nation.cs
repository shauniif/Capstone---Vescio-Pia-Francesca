 using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Entity
{
    public class Nation : DescribedModifiableEntity
    {
        public string FormOfGovernment { get; set; }
        public string Photo { get; set; }

        public List<City> Cities { get; set; } = new List<City>();
        public List<Eco> Ecos { get; set; } = new List<Eco>();
        public List<Guild> Guilds { get; set; } = new List<Guild>();

    }
}
