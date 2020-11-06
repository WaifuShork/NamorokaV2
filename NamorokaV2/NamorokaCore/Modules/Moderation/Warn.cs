using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;
using Discord;
using NamorokaV2.Attributes;

namespace NamorokaV2
{
    [RequireContext(ContextType.Guild)]
    public sealed partial class Moderation    
    {
        /*[Command("nick"), Priority(1)]
        [Summary("Change your nickname to the specified text")]
        [RequireUserPermission(GuildPermission.ChangeNickname)]
        public Task Nick([Remainder]string name) => Nick(Context.User as SocketGuildUser, name);*/

        [Command("warn"), Priority(1)]
        [Summary("Warns a user with a reason")]
        [Remarks("-warn <user> <reason>")]
        [RequireContext(ContextType.Guild)]

        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task Warn(SocketGuildUser user, [Remainder] string reason)
        {
            SocketUserMessage message = Context.Message;
            await Context.Channel.DeleteMessageAsync(message);
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithAuthor(user);
            builder.WithColor(Color.Red);
            builder.AddField("Reason", reason);
            builder.WithCurrentTimestamp();
            
            Embed embed = builder.Build();

            await Context.Channel.SendMessageAsync(null, false, embed);
        }
    }
}