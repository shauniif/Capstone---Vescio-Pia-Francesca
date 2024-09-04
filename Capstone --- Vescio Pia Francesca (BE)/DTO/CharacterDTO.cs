using Capstone_____Vescio_Pia_Francesca__BE_.Entity;
using System.ComponentModel.DataAnnotations;

    namespace Capstone_____Vescio_Pia_Francesca__BE_.DTO
    {
            public class CharacterDTO
            {
                public int Id { get; set; }

                [MaxLength(50)]
                public string Name { get; set; }

                public int? GuildId { get; set; }

                public int CityId { get; set; }

                public int RaceId { get; set; }

                public int? EcoId { get; set; }
                public int UserId  { get; set; }
                public IFormFile Image { get; set; }
            }
    }
