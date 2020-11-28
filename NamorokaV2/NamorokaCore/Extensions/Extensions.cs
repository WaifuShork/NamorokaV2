using System.Threading.Tasks;
using Discord;
using Discord.Rest;
using Discord.WebSocket;

namespace NamorokaV2.NamorokaCore.Extensions
{
    public static class Extensions
    {
        public static async Task SendLogMessageAsync(Embed embed)
        {
            const ulong guildId = ChannelIds.GuildId;
            const ulong logChannelId = ChannelIds.LogChannelId;
            SocketTextChannel channel = CommandHandler._client.GetGuild(guildId).GetTextChannel(logChannelId);
            RestUserMessage message = await channel.SendMessageAsync(embed: embed);
        }
        
        public static async Task SendLogMessageAsync(string msg)
        {
            const ulong guildId = ChannelIds.GuildId;
            const ulong logChannelId = ChannelIds.LogChannelId;
            SocketTextChannel channel = CommandHandler._client.GetGuild(guildId).GetTextChannel(logChannelId);
            RestUserMessage message = await channel.SendMessageAsync(msg);
        }
    }
}