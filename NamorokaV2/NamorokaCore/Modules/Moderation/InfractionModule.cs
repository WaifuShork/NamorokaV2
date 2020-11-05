using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Newtonsoft.Json;

namespace NamorokaV2
{
    public sealed class InfractionModule : ModuleBase<SocketCommandContext>
    {
        [Command("infr")]
        public async Task InfractionAsync()
        {
            string json = await File.ReadAllTextAsync(JsonService._loggingJson);
            dynamic jsonObj = JsonConvert.DeserializeObject(json);
            string user = jsonObj["user"];
            string reason = jsonObj["reason"];
            string id = jsonObj["id"];
            
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithAuthor(user);
            builder.AddField("Reason", reason);
            builder.WithFooter(id);
            builder.WithColor(Color.Red);
            builder.WithCurrentTimestamp();
            Embed embed = builder.Build();
            
            await Context.Channel.SendMessageAsync(null, false, embed);
        }
    }
}