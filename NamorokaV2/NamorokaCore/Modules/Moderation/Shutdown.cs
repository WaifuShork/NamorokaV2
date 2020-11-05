using Discord.Commands;
using System.Threading.Tasks;
using NamorokaV2.Attributes;

namespace NamorokaV2
{
    [RequireContext(ContextType.Guild)]
    public sealed class ShutdownModule : ModuleBase<SocketCommandContext>
    {
        [Command("shutdown")]
        [Summary("turns the bot off")]
        [RequireRole(RoleIds.Administrator)]
        public async Task ShutdownAsync()
        {
            await Context.Channel.SendMessageAsync("``Powering down...``");
            await Context.Client.StopAsync();
            await Context.Client.LogoutAsync();
        }
    }
}