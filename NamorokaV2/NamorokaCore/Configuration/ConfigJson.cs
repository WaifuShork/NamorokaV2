using Newtonsoft.Json;

namespace NamorokaV2.Configuration
{
    internal struct ConfigJson
    {
        [JsonProperty("token")] internal string Token { get; private set;}
        [JsonProperty("prefix")] internal string Prefix { get; set;}
    }
}