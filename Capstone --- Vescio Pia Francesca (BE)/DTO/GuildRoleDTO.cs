using System.ComponentModel.DataAnnotations;

namespace Capstone_____Vescio_Pia_Francesca__BE_.DTO
{
    public class GuildRoleDTO
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        public int GuildId { get; set; }

        public decimal Modifier { get; set; }
    }
}
