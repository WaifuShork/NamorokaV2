using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using NamorokaV2.Attributes;

namespace NamorokaV2
{
    public sealed class KickModule : ModuleBase<SocketCommandContext>
    {
        [RequireRole(RoleIds.Administrator)]
        [RequireUserPermission(GuildPermission.KickMembers)]
        [Command("kick")]
        public async Task KickAsync(SocketGuildUser user)
        {
            await user.KickAsync();
            await Context.Channel.SendMessageAsync($"{user} has been removed from this hell hole.");
        }
    }
}