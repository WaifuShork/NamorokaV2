using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using NamorokaV2.Attributes;

namespace NamorokaV2
{
    public sealed partial class Moderation    
    {
        [RequireUserPermission(GuildPermission.Administrator)]
        [RequireUserPermission(GuildPermission.KickMembers)]
        [Command("kick")]
        public async Task KickAsync(SocketGuildUser user)
        {
            SocketUserMessage message = Context.Message;
            await Context.Channel.DeleteMessageAsync(message);
            await user.KickAsync();
            await Context.Channel.SendMessageAsync($"{user} has been removed from this hell hole.");
        }
    }
}