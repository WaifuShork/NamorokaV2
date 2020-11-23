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
        //random.json?limit=1
            HttpClient client = new HttpClient();
            string result = await client.GetStringAsync("https://rule34.xxx/index.php");
            JArray arr = JArray.Parse(result);
            JObject post = JObject.Parse(arr[0]["data"]["children"][0]["data"].ToString());
            EmbedBuilder builder = new EmbedBuilder()
                .WithImageUrl(post["url"].ToString())
                .WithColor(new Color(33, 176, 252))
                .WithTitle(post["title"].ToString());
                //.WithUrl("https://reddit.com" + post["permalink"]);
            Embed embed = builder.Build();
            await Context.Channel.SendMessageAsync(embed: embed);
        }
    }
}