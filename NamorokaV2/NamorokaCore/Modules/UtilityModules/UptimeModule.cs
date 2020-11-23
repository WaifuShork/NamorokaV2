using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using Humanizer;
using Humanizer.Localisation;

namespace NamorokaV2
{
    public class UptimeModule : ModuleBase<SocketCommandContext>
    {
        [Command("uptime")]
        [Summary("Displays the time the bot has been running for.")]
        public async Task DisplayUptimeAsync()
        {
            SocketUserMessage message = Context.Message;
            await Context.Channel.DeleteMessageAsync(message);
            TimeSpan uptime = DateTime.Now - Process.GetCurrentProcess().StartTime;

            await ReplyAsync($"I've been up and running for {uptime.Humanize(precision: 3, minUnit: TimeUnit.Second)}");
        }
    }
}