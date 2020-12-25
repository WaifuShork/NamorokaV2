using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Extensions = NamorokaV2.NamorokaCore.Extensions.Extensions;

namespace NamorokaV2
{
    public sealed partial class Moderation    
    {
        [Command("unmute")]
        [Summary("unmute a user.")]
        [Remarks("-unmute <user>")]
        [RequireUserPermission(GuildPermission.Administrator)]

        public async Task UnmuteAsync(IGuildUser user)
        {
            SocketUserMessage message = Context.Message;
            await Context.Channel.DeleteMessageAsync(message);
            await user.UnmuteAsync(Context);
            await Extensions.SendLogMessageAsync($"{user} has been unmuted.");
        }
    }
}