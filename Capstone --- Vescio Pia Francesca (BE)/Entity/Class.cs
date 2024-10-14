namespace Capstone_____Vescio_Pia_Francesca__BE_.Entity
{
    public class CharacterGuildRole : BaseEntity
    {
        
        public Character Character { get; set; }

        
        public Guild Guild { get; set; }

        
        public GuildRole GuildRole { get; set; }
    }
}
