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
            const string reason = "None";
            SocketUserMessage message = Context.Message;
            await Context.Channel.DeleteMessageAsync(message);
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithAuthor($"[Baned User] {user}", user.GetAvatarUrl());
            builder.AddField("Reason", reason);
            builder.WithColor(Color.Red);
            builder.WithCurrentTimestamp();
            Embed embed = builder.Build();
            await Context.Guild.AddBanAsync(user);
            await logsAsync.SendLogMessageAsync(embed);
        }
        
        [RequireUserPermission(GuildPermission.Administrator)]
        [Command("ban")]
        [Summary("Permanently bans a specified user")]
        [Remarks("-ban <user> <reason>")]
        public async Task BanAsync(IUser user, [Remainder]string reason)
        {
            SocketUserMessage message = Context.Message;
            await Context.Channel.DeleteMessageAsync(message);
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithAuthor($"[Banned User] {user}, user.GetAvatarUrl()");
            builder.AddField("Reason", reason);
            builder.WithColor(Color.Red);
            builder.WithCurrentTimestamp();
            Embed embed = builder.Build();
            await Context.Guild.AddBanAsync(user);
            await logsAsync.SendLogMessageAsync(embed);
        }
    }
}
