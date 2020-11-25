using System.Threading.Tasks;
using Discord;

namespace NamorokaV2
{
    public sealed partial class Moderation
    {
        private async Task SendLog(Embed embed)
        {
            const ulong guildId = ChannelIds.GuildId;
            const ulong logChannelId = ChannelIds.LogChannelId;
            ITextChannel channel = Context.Client.GetGuild(guildId).GetTextChannel(logChannelId);
            if (channel != null)
            {
                await channel.SendMessageAsync(embed: embed);
            }
        }
        
        private async Task SendLog(string message)
        {
            const ulong guildId = ChannelIds.GuildId;
            const ulong logChannelId = ChannelIds.LogChannelId;
            ITextChannel channel = Context.Client.GetGuild(guildId).GetTextChannel(logChannelId);
            if (channel != null)
            {
                await channel.SendMessageAsync(message);
            }
        }
    }
}