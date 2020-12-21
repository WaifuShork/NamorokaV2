/*using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using NamorokaV2.Attributes;
using Newtonsoft.Json;
using Discord.Addons.Interactive;

namespace NamorokaV2
{
    public sealed class BeeModule : ModuleBase<SocketCommandContext>
    {
        [Command("bee")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task SendBeeMovieAsync()
        {
            string initializeJson = await File.ReadAllTextAsync(data);
            List<string> bee = JsonConvert.DeserializeObject<List<string>>(initializeJson);
            bee[];
        }
    }
}*/
