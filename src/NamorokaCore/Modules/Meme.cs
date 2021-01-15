using System.Net.Http;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Newtonsoft.Json.Linq;

namespace NamorokaV2.NamorokaCore.Modules
{
    public class Meme : ModuleBase<SocketCommandContext>
    {
        [Command("meme")]
        public async Task SendMemeAsync()
        {
            var client = new HttpClient();
            var result = await client.GetStringAsync("https://reddit.com/r/memes/random.json?limit=1");
            var arr = JArray.Parse(result);
            var post = JObject.Parse(arr[0]["data"]["children"][0]["data"].ToString());
            var builder = new EmbedBuilder()
                .WithImageUrl(post["url"].ToString())
                .WithColor(new Color(33, 176, 252))
                .WithTitle(post["title"].ToString())
                .WithUrl("https://reddit.com" + post["permalink"]);
            var embed = builder.Build();
            await Context.Channel.SendMessageAsync(embed: embed);
        }
    }
}