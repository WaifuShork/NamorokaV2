using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using NamorokaV2.Configuration;

namespace NamorokaV2.NamorokaCore.Extensions
{
    internal static class SendLogsAsync
    {
        private static readonly DiscordSocketClient client = new();
        internal static async Task<IMessage> SendLogMessageAsync(this ITextChannel socketChannel, string messageString)
        {
            
            const ulong guildId = ChannelIds.GuildId;
            const ulong logChannelId = ChannelIds.LogChannelId;
        
            var channel = client.GetGuild(guildId).GetTextChannel(logChannelId);

            var message = await channel.SendMessageAsync(messageString);
            return message;
        }
        
        internal static async Task<IMessage> SendLogMessageAsync(this ITextChannel channel, Embed embed)
        {
            const ulong guildId = ChannelIds.GuildId;
            const ulong logChannelId = ChannelIds.LogChannelId;
        
            channel = client.GetGuild(guildId).GetTextChannel(logChannelId);

            var message = await channel.SendMessageAsync(embed: embed);
            return message;
        }
    }
}