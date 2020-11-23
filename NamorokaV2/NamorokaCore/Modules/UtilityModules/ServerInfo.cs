using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace NamorokaV2
{
    public class ServerInfo : ModuleBase<SocketCommandContext>
    {
        [Command("info")]
        public async Task Info()
        {
            EmbedBuilder builder = new EmbedBuilder()
                .WithThumbnailUrl(Context.Guild.IconUrl)
                .WithDescription("In this message you can find some information about the current server.")
                .WithTitle($"{Context.Guild.Name} Information")
                .WithColor(new Color(33, 176, 252))
                .AddField("Created at", Context.Guild.CreatedAt.ToString("dd/MM/yyyy"), true)
                .AddField("Member count", (Context.Guild as SocketGuild).MemberCount + " members", true)
                .AddField("Online users", Context.Guild.Users.Count(x => x.Status != UserStatus.Offline) + " members", true);
            Embed embed = builder.Build();
            await Context.Channel.SendMessageAsync(embed: embed);
        }
    }
}