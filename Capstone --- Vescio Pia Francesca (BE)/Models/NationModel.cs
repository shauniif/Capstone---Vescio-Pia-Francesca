using Capstone_____Vescio_Pia_Francesca__BE_.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Models
{
    public class NationModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string FormOfGovernment { get; set; }
        public IFormFile Photo { get; set; }
        public decimal Modifier { get; set; }

        public List<City> Cities { get; set; } = new List<City>();
        public List<Eco> Ecos { get; set; } = new List<Eco>();
        public List<Guild> Guilds { get; set; } = new List<Guild>();
    }
}
