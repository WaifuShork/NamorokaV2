using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using NamorokaV2.NamorokaCore.Extensions;

namespace NamorokaV2.NamorokaCore.Modules.UtilityModules
{
    public class SystemTestModule : ModuleBase<SocketCommandContext>
    {
        [Command("system-test")]
        [Alias("sys-test", "test", "sys-inf")]
        [Summary("Displays the time the bot has been running for.")]
        public async Task SystemTestAsync()
        {
            int latency = Context.Client.Latency;
            EmbedBuilder builder = new EmbedBuilder()
                .WithAuthor("Namoroka System Analyzer")
                .WithTitle("System Test Results")
                .WithDescription("Here are all the results of my system test, if you're reading this message, my systems are most likely online")
                .WithThumbnailUrl("https://lh3.googleusercontent.com/proxy/HR0AupsvGNddHdnjHK3HbUQp5YHDHiJZT3ijMNzni9uZybwagvR6eEKWmwNKAhwau6m8IsdOf1RsY1S1ZH8EII8G646HiDLjO-UbX8wQs5dgBEEWwqE")
                .WithColor(Color.Purple)
                .AddField("Latency :bar_chart:", $"Current latency is: {latency}ms", inline: true)
                .AddField("What is this?", "This has been a test executed by the user to check my performance", true)
                .AddField("Accuracy?", "If you feel these results are wrong, please immediately contact my administrators");
            await builder.Build().SendToChannel(Context.Channel);
        }
    }
}