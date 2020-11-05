using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using NamorokaV2.Attributes;
using Newtonsoft.Json;

// ReSharper disable once ClassNeverInstantiated.Global

namespace NamorokaV2
{
    public sealed class ConfigModule : ModuleBase<SocketCommandContext>
    {
        [RequireRole(RoleIds.Administrator)]
        [Command("prefix")]
        public async Task PrefixAsync(string prefix)
        {
            string json = await File.ReadAllTextAsync(JsonService._configJson);
            dynamic jsonObj = JsonConvert.DeserializeObject(json);
            jsonObj["prefix"] = prefix;
            string output = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
            await File.WriteAllTextAsync(JsonService._configJson, output);
            
            await Context.Channel.SendMessageAsync($"I changed your prefix to: {prefix}");
        }
    }
}  