using System.Collections.Generic;

namespace Otium.OmdbProvider.Entities
{
    public enum EntityType
    {
        Movie,
        Series,
        Episode
    }

    public class EntityBase
    {
        public string Title { get; set; }
        public string Year { get; set; }
        public string Rated { get; set; }
        public string Released { get; set; }
        public List<Rating> Ratings { get; set; }
        public string Metascore { get; set; }
        public string ImdbRating { get; set; }
        public string ImdbVotes { get; set; }
        public string ImdbID { get; set; }
    }
}
