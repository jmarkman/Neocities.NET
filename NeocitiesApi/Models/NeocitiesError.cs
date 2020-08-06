using Newtonsoft.Json;

namespace NeocitiesApi.Models
{
    public class NeocitiesError
    {
        [JsonProperty("result")]
        public string Result { get; set; }
        [JsonProperty("error_type")]
        public string Type { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
