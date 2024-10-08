﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Entity
{
    public class Event : BaseEntity
    {
      

        [MaxLength(150)]
        public string Name { get; set; }
        public string Cover { get; set; }
        public DateTime Date { get; set; }

        public int YearOfEvent { get; set; }

        public string Description { get; set; }

        public string Influence { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Modifier { get; set; }

        public bool? IsChanged { get; set; }

        public string ShortDescription
        {
            get
            {
                if (string.IsNullOrEmpty(Description))
                {
                    return string.Empty;
                }

                if (Description.Length > 75)
                {
                    return Description.Substring(0, 75) + "...";
                }

                return Description;
            }
        }
    }
}
