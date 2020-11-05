using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using NamorokaV2.Attributes;

namespace NamorokaV2
{
    public sealed class MuteModule : ModuleBase<SocketCommandContext>
    {
        [Command("mute")]
        [Summary("Mutes a user with a specified reason.")]
        [Remarks("-muted <user> <reason>")]
        [RequireRole(RoleIds.Administrator)]
        public async Task MuteAsync(SocketGuildUser user, [Remainder]string reason)
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithAuthor(user);
            builder.AddField("Reason", reason);
            builder.WithColor(Color.Red);
            builder.WithCurrentTimestamp();
            Embed embed = builder.Build();

            //SocketRole role = Context.Guild.Roles.FirstOrDefault(x => x.Name.ToString() == /*add muted role modularity*/ "Muted");
            SocketRole role = Context.Guild.GetRole(RoleIds.Muted);
            await ((IGuildUser)user).AddRoleAsync(role);
            await Context.Channel.SendMessageAsync(null, false, embed);
        }
    }
}
