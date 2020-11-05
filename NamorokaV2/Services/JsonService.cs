using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NamorokaV2
{
    internal sealed class JsonService
    {
        public static string _configJson { get; } = "G:/source/NamorokaV2/NamorokaV2/NamorokaV2/bin/Debug/netcoreapp3.1/configuration/config.json";
        public static string _loggingJson { get; } = "G:/source/NamorokaV2/NamorokaV2/NamorokaV2/bin/Debug/netcoreapp3.1/logs/infractions.json";
        
        internal static async Task<ConfigJson> GetConfigJson(string _json)
        {
            await using (FileStream fileStream = File.OpenRead(_json))
            using (StreamReader streamReader = new StreamReader(fileStream, new UTF8Encoding(false)))
                _json = await streamReader.ReadToEndAsync().ConfigureAwait(false);
            ConfigJson configJson = JsonConvert.DeserializeObject<ConfigJson>(_json);
            return configJson;
        }
    }
}