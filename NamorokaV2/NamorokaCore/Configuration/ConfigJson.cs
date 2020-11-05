using Newtonsoft.Json;

namespace NamorokaV2
{
    internal struct ConfigJson
    {
        [JsonProperty("token")] internal string Token { get; private set;}
        [JsonProperty("prefix")] internal string Prefix { get; set;}
    }
}