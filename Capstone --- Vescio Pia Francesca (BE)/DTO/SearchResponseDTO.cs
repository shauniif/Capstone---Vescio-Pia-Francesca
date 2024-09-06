using Capstone_____Vescio_Pia_Francesca__BE_.Entity;

namespace Capstone_____Vescio_Pia_Francesca__BE_.DTO
{
    public class SearchResponseDTO
    {
        public List<Eco> Ecos {  get; set; } = new List<Eco>();

        public List<Guild> Guilds { get; set; } = new List<Guild>();
        public List<City> Cities { get; set; } = new List<City>();

        public List<Nation> Nations { get; set; } = new List<Nation>();
    }
}
