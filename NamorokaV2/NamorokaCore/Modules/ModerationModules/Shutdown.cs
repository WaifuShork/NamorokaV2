using System.Net.Mime;
using Discord.Commands;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using NamorokaV2.Attributes;

namespace NamorokaV2
{
    [RequireContext(ContextType.Guild)]
    public sealed partial class Moderation    
    {
        [Command("shutdown")]
        [Summary("turns the bot off")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task ShutdownAsync()
        {
            SocketUserMessage message = Context.Message;
            await Context.Channel.DeleteMessageAsync(message);
            
            await Context.Channel.SendMessageAsync("``Powering down...``");
            await Context.Client.StopAsync();
            await Context.Client.LogoutAsync();
        }
    }
}