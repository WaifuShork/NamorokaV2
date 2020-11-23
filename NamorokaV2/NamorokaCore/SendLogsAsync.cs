using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace NamorokaV2.NamorokaCore
{
    internal class SendLogsAsync : ModuleBase<SocketCommandContext>
    {
        internal async Task SendLogMessageAsync(string message)
        {
            const ulong guildId = ChannelIds.GuildId;
            const ulong logChannelId = ChannelIds.LogChannelId;
        
            ITextChannel channel = Context.Client.GetGuild(guildId).GetTextChannel(logChannelId);

            if (channel != null && message != null)
            {
                await channel.SendMessageAsync(message);
            }
        }
        
        internal async Task SendLogMessageAsync(Embed embed)
        {
            const ulong guildId = ChannelIds.GuildId;
            const ulong logChannelId = ChannelIds.LogChannelId;
        
            ITextChannel channel = Context.Client.GetGuild(guildId).GetTextChannel(logChannelId);

            if (channel != null && embed != null)
            {
                await channel.SendMessageAsync(embed: embed);
            }
        }
    }
}