using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;

namespace NamorokaV2.NamorokaCore.Modules.UtilityModules.ConfigModules
{
    public sealed partial class ConfigModule : ModuleBase<SocketCommandContext>
    {
        public static IConfigurationRoot _config;

        [Command("prefix")]
        public async Task PrefixAsync(string _prefix)
        {
            SocketUserMessage message = Context.Message;
            await Context.Channel.DeleteMessageAsync(message);
            //string json = await File.ReadAllTextAsync(JsonService._configJson);
            //dynamic jsonObj = JsonConvert.DeserializeObject(json);
            //jsonObj["prefix"] = _prefix;
            //string output = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
            //await File.WriteAllTextAsync(JsonService._configJson, output);
            _config["prefix"] = _prefix;

            
            await Context.Channel.SendMessageAsync($"I changed your prefix to: {_prefix}");
        }
    }
}  