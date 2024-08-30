using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Capstone_____Vescio_Pia_Francesca__BE_.DTO
{
    public class NationDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string FormOfGovernment { get; set; }
        public string Photo { get; set; }

        public decimal Modifier { get; set; }
    }
}
