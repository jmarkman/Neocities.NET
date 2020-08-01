using System.Collections.Generic;
using Newtonsoft.Json;

namespace NeocitiesApi.Models
{
    public class NeocitiesWebsiteInfo : NeocitiesWebsiteBase
    {
        [JsonProperty("info")]
        public Attributes Attributes { get; set; }
    }

    public class Attributes
    {
        [JsonProperty("sitename")]
        public string SiteName { get; set; }
        [JsonProperty("hits")]
        public int NumberOfHits { get; set; }
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }
        [JsonProperty("last_updated")]
        public string LastUpdated { get; set; }
        [JsonProperty("domain")]
        public string Domain { get; set; }
        [JsonProperty("tags")]
        public List<string> Tags { get; set; }
    }

}
