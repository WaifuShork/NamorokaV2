using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using NamorokaV2.Attributes;

namespace NamorokaV2
{
    public sealed class BanModule : ModuleBase<SocketCommandContext>
    {
        [RequireRole(RoleIds.Administrator)]
        [Command("ban")]
        [Summary("Permanently bans a specified user")]
        [Remarks("-ban <user>")]
        public async Task BanAsync(IUser user)
        {
            await Context.Guild.AddBanAsync(user);
        }
    }
}
