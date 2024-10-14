namespace Capstone_____Vescio_Pia_Francesca__BE_.Entity
{
    public class GuildRole : ModifiableEntity
    {
        public Guild? Guild { get; set; }
        public List<Character> Characters { get; set; } = new List<Character>();
    }
}
