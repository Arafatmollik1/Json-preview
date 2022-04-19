using Newtonsoft.Json;

namespace HomeCare.Models
{
    public class SocialService
    {
        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("codeset")]
        public string Codeset { get; set; }

        [JsonProperty("code")]
        public int Code { get; set; }
    }
}
