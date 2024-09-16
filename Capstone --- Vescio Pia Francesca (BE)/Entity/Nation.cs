 using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Entity
{
    public class Nation
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        public string Description { get; set; }

        public string FormOfGovernment { get; set; }
        public string Photo { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Modifier { get; set; }
        public List<City> Cities { get; set; } = new List<City>();
        public List<Eco> Ecos { get; set; } = new List<Eco>();
        public List<Guild> Guilds { get; set; } = new List<Guild>();


        public string ShortDescription
        {
            get
            {
                if (string.IsNullOrEmpty(Description))
                {
                    return string.Empty;
                }

                if (Description.Length > 50)
                {
                    return Description.Substring(0, 50) + "...";
                }

                return Description;
            }
        }
    }
}
