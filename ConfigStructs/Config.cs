using Newtonsoft.Json;

namespace faxnocapBPbot
{
    public struct Config
    {
        [JsonProperty("token")]
        public string Token { get; private set; }
        [JsonProperty("prefix")]
        public string Prefix { get; private set; }
    }
}
