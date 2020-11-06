using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace NamorokaV2
{
    public sealed partial class Moderation : ModuleBase<SocketCommandContext>
    {
        [RequireUserPermission(GuildPermission.Administrator)]
        [Command("ban")]
        [Summary("Permanently bans a specified user")]
        [Remarks("-ban <user>")]
        public async Task BanAsync(IUser user)
        {
            SocketUserMessage message = Context.Message;
            await Context.Channel.DeleteMessageAsync(message);
            await Context.Guild.AddBanAsync(user);
        }
    }
}
