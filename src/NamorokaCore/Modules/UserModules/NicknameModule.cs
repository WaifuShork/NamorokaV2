using Discord.Commands;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace NamorokaV2.NamorokaCore.Modules.UserModules
{
    [Name("Nickname")]
    [RequireContext(ContextType.Guild)]
    public sealed class NicknameModule : ModuleBase<SocketCommandContext>
    {
        /*[Command("nick"), Priority(1)]
        [Summary("Change your nickname to the specified text")]
        [RequireUserPermission(GuildPermission.ChangeNickname)]
        public Task Nick([Remainder]string name) => Nick(Context.User as SocketGuildUser, name);*/

        [Command("nick"), Priority(1)]
        [Summary("Change another user's nickname to the specified text")]
        // [RequireUserPermission(GuildPermission.ManageNicknames)]
        public async Task Nick(IGuildUser user, [Remainder]string name)
        {
            SocketUserMessage message = Context.Message;
            IUser userContext = Context.Guild.CurrentUser;
            if (userContext.Id == user.Id)
                return;
            await user.ModifyAsync(x => x.Nickname = name);
            await Context.Channel.DeleteMessageAsync(message);
            await ReplyAsync($"{user.Mention} I changed your name to **{name}**");
        }
    }
}