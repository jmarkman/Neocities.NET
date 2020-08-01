using Newtonsoft.Json;

namespace NeocitiesApi.Models
{
    public class NeocitiesWebsiteBase
    {
        [JsonProperty("result")]
        public string Result { get; set; }
    }
}
