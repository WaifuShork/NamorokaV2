using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using NamorokaV2.Attributes;
using Newtonsoft.Json;

namespace NamorokaV2
{
    public sealed partial class ConfigModule : ModuleBase<SocketCommandContext>
    {
        [RequireRole(RoleIds.Administrator)]
        [Command("prefix")]
        public async Task PrefixAsync(string _prefix)
        {
            SocketUserMessage message = Context.Message;
            await Context.Channel.DeleteMessageAsync(message);
            string json = await File.ReadAllTextAsync(JsonService._configJson);
            dynamic jsonObj = JsonConvert.DeserializeObject(json);
            jsonObj["prefix"] = _prefix;
            string output = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
            await File.WriteAllTextAsync(JsonService._configJson, output);
            
            await Context.Channel.SendMessageAsync($"I changed your prefix to: {_prefix}");
        }
    }
}  