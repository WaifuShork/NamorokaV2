using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace NamorokaV2.NamorokaCore
{
    internal static class SendLogsAsync
    {
        private static readonly DiscordSocketClient client = new();
        internal static async Task<IMessage> SendLogMessageAsync(this ISocketMessageChannel socketChannel, string messageString)
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