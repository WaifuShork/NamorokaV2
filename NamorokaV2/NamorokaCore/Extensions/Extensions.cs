using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace NamorokaV2.NamorokaCore.Extensions
{
    public static class Extensions
    {
        private static DiscordSocketClient client;
        public static async Task<IMessage> SendSuccessAsync(this ISocketMessageChannel channel, string title, string description)
        {

            var embed = new EmbedBuilder()
                .WithColor(Color.Red)
                .WithTitle(title)
                .WithDescription(description)
                .Build();
            var message = await channel.SendMessageAsync(embed: embed);
            return message;
        }
    }
}