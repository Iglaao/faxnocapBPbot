using Newtonsoft.Json;

namespace faxnocapBPbot.ConfigStructs
{
    public struct FirestoreConfig
    {
        [JsonProperty("type")]
        public string Type { get; private set; }
        [JsonProperty("project_id")]
        public string Project_id { get; private set; }
        [JsonProperty("private_key_id")]
        public string Private_key_id { get; private set; }
        [JsonProperty("private_key")]
        public string Private_key { get; private set; }
        [JsonProperty("client_email")]
        public string Client_email { get; private set; }
        [JsonProperty("client_id")]
        public string Client_id { get; private set; }
        [JsonProperty("auth_uri")]
        public string Auth_uri { get; private set; }
        [JsonProperty("token_uri")]
        public string Token_uri { get; private set; }
        [JsonProperty("auth_provider_x509_cert_url")]
        public string Tokeauth_provider_x509_cert_urln_uri { get; private set; }
        [JsonProperty("client_x509_cert_url")]
        public string Client_x509_cert_url { get; private set; }
        [JsonProperty("universe_domain")]
        public string Universe_domain { get; private set; }
    }
}
