using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace NeocitiesApi.Models
{
    public class NeocitiesFileList : NeocitiesWebsiteBase
    {
        [JsonProperty("files")]
        public List<File> Files { get; set; }
    }

    public class File
    {
        [JsonProperty("path")]
        public string Path { get; set; }
        [JsonProperty("is_directory")]
        public bool IsDirectory { get; set; }
        [JsonProperty("size")]
        public double Size { get; set; }
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
        [JsonProperty("sha1_hash")]
        public string SHA1Hash { get; set; }
    }

}
