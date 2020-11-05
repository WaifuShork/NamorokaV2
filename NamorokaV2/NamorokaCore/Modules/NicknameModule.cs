﻿using Discord.Commands;
using System.Threading.Tasks;
using Discord;

namespace NamorokaV2
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
        public async Task Nick(IUser user, [Remainder]string name)
        {
            IUser userContext = Context.Guild.CurrentUser;
            if (userContext.Id == user.Id)
                return;
            await ((IGuildUser) user).ModifyAsync(x => x.Nickname = name);
            await ReplyAsync($"{user.Mention} I changed your name to **{name}**");
        }
    }
}