

namespace WideBotProject.Models
{
    public class WideBotAPI
    {
        public Attributes attributes { get; set; }
        public Support flowName { get; set; }
        public List<FacebookCard> FacebookResponse { get; set; }

    }

    public class Attributes
    {
        public int page { get; set; }
        public int per_page { get; set; }
        public int total { get; set; }
        public int total_pages { get; set; }

    }

}
