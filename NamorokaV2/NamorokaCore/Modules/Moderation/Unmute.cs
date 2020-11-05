using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using NamorokaV2.Attributes;

namespace NamorokaV2
{
    public sealed class UnmuteModule : ModuleBase<SocketCommandContext>
    {
        [Command("unmute")]
        [Summary("unmute a user.")]
        [Remarks("-unmute <user>")]
        [RequireRole(RoleIds.Administrator)]

        public async Task UnmuteAsync(SocketGuildUser user)
        {
            //SocketRole role = Context.Guild.Roles.FirstOrDefault(x => x.Name.ToString() == "Muted");
            SocketRole role = Context.Guild.GetRole(RoleIds.Muted);
            await ((IGuildUser)user).RemoveRoleAsync(role);
            await Context.Channel.SendMessageAsync($"{user} has been unmuted!");
        }
    }
}