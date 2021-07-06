using Newtonsoft.Json;

namespace Otium.OmdbProvider.Entities
{
    public class Episode : EntityBase
    {
        public string Season { get; set; }
        [JsonProperty("episode")]
        public string EpisodeNumber { get; set; }
        public string Runtime { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public string Writer { get; set; }
        public string Actors { get; set; }
        public string Plot { get; set; }
        public string Language { get; set; }
        public string Country { get; set; }
        public string Awards { get; set; }
        public string Poster { get; set; }
        public string SeriesID { get; set; }
        public string Type { get; set; }
        public string Response { get; set; }
    }
}
