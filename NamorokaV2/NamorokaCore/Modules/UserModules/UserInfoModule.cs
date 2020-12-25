using System.Linq;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using Discord.WebSocket;
using NamorokaV2.NamorokaCore.Extensions;


namespace NamorokaV2.NamorokaCore.Modules.UserModules
{
    public class UserInfoModule : ModuleBase<SocketCommandContext>
    {
        [Command("userinfo")]
        [Summary("Displays information about a specified user.")]
        [Remarks("-userinfo <user>")]
        public async Task ShowUserInfoAsync(IGuildUser user)
        {
            SocketUserMessage message = Context.Message;
            await Context.Channel.DeleteMessageAsync(message);
            Color userColor = Color.Magenta;
            if (user.RoleIds.Count > 1)
                userColor = user.RoleIds
                    .Select(id => Context.Guild.GetRole(id))
                    .OrderByDescending(role => role.Position)
                    .First().Color;


            EmbedBuilder builder = new EmbedBuilder()
                .WithTitle($"Information about {user}:")
                .WithThumbnailUrl(user.GetAvatarUrl())
                .WithColor(userColor)
                .AddField("Username", user.ToString(), true)
                .AddField("ID", user.Id.ToString(), true)
                .AddFieldConditional(!string.IsNullOrEmpty(user.Nickname), "Nickname", user.Nickname, true)
                .AddFieldConditional(user.JoinedAt.HasValue, "Join Date", user.JoinedAt?.ToShortDateString(), true)
                .AddField("User Created", user.CreatedAt.ToShortDateString(), true);
            await builder.Build().SendToChannel(Context.Channel);
        }
    }
}