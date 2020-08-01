using Newtonsoft.Json;

namespace NeocitiesApi.Models
{
    public class NeocitiesWebsiteApiKey : NeocitiesWebsiteBase
    {
        [JsonProperty("api_key")]
        public string ApiKey { get; set; }
    }
}
