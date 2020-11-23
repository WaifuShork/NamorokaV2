using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using NamorokaV2.Attributes;

namespace NamorokaV2
{
    public sealed partial class Moderation    
    {
        [Command("unmute")]
        [Summary("unmute a user.")]
        [Remarks("-unmute <user>")]
        [RequireUserPermission(GuildPermission.Administrator)]

        public async Task UnmuteAsync(SocketGuildUser user)
        {
            SocketUserMessage message = Context.Message;
            await Context.Channel.DeleteMessageAsync(message);
            SocketRole role = Context.Guild.GetRole(RoleIds.Muted);
            await ((IGuildUser)user).RemoveRoleAsync(role);
            await logsAsync.SendLogMessageAsync($"{user} has been unmuted.");
        }
    }
}