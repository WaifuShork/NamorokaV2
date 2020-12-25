using System.Threading.Tasks;
using Discord;
using NamorokaV2.Configuration;
using NamorokaV2.NamorokaCore.Services;

namespace NamorokaV2.NamorokaCore.Extensions
{
    public static class Extensions
    {
        public static async Task SendLogMessageAsync(Embed embed)
        {
            const ulong guildId = ChannelIds.GuildId;
            const ulong logChannelId = ChannelIds.LogChannelId;
            var channel = CommandHandler._client.GetGuild(guildId).GetTextChannel(logChannelId);
            await channel.SendMessageAsync(embed: embed);
        }
        
        public static async Task SendLogMessageAsync(string msg)
        {
            const ulong guildId = ChannelIds.GuildId;
            const ulong logChannelId = ChannelIds.LogChannelId;
            var channel = CommandHandler._client.GetGuild(guildId).GetTextChannel(logChannelId);
            await channel.SendMessageAsync(msg);
        }
    }
}