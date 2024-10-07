namespace Capstone_____Vescio_Pia_Francesca__BE_.Entity
{
    public class DescribedEntity : NamedEntity
    {
        public string Description { get; set; }
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
