using Discord.Commands;
using System.Threading.Tasks;

namespace NamorokaV2.NamorokaCore.Modules.Moderation
{
    [RequireContext(ContextType.Guild)]
    public sealed partial class Moderation    
    {
        [Command("shutdown")]
        [Summary("turns the bot off")]
        [RequireOwner]
        public async Task ShutdownAsync()
        {
            var message = Context.Message;
            await Context.Channel.DeleteMessageAsync(message);
            
            await Context.Channel.SendMessageAsync("``Powering down...``");
            await Context.Client.StopAsync();
            await Context.Client.LogoutAsync();
        }
    }
}