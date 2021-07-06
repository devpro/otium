using System.Collections.Generic;

namespace Otium.OmdbProvider.Entities
{
    public class SearchResult
    {
        public List<Search> Search { get; set; }
        public string TotalResults { get; set; }
        public string Response { get; set; }
    }
}
