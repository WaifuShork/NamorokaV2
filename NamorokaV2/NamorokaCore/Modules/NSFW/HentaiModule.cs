using System.Net.Http;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Newtonsoft.Json.Linq;

namespace NamorokaV2
{
    public class HentaiModule : ModuleBase<SocketCommandContext>
    {
        [Command("hentai")]
        [RequireNsfw]
        public async Task SendHentaiAsync()
        {
            var client = new HttpClient();
            var result = await client.GetStringAsync("https://www.reddit.com/r/hentai/random.json?limit=1");
            var arr = JArray.Parse(result);
            var post = JObject.Parse(arr[0]["data"]["children"][0]["data"].ToString());
            var builder = new EmbedBuilder()
                .WithImageUrl(post["url"].ToString())
                .WithColor(new Color(33, 176, 252));
            var embed = builder.Build();
            await Context.Channel.SendMessageAsync(embed: embed);
        }
    }
}