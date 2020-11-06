using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Newtonsoft.Json;

namespace NamorokaV2
{
    public sealed partial class Moderation    
    {
        [Command("infraction")]
        [Alias("infr", "inf", "warnings", "warns")]
        public async Task InfractionAsync(SocketGuildUser user)
        {
            SocketUserMessage message = Context.Message;
            await Context.Channel.DeleteMessageAsync(message);
            
            string json = await File.ReadAllTextAsync(JsonService._loggingJson);
            dynamic jsonObj = JsonConvert.DeserializeObject(json);
            user = jsonObj["user"];
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