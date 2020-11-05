using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using NamorokaV2.Attributes;
using Newtonsoft.Json;

// ReSharper disable once ClassNeverInstantiated.Global

namespace NamorokaV2
{
    public sealed class InfoModule : ModuleBase<SocketCommandContext>
    {
        [Command("say")]
        [Summary("Echoes a message.")]
        public async Task SayAsync([Remainder] [Summary("The text to echo")] string echo)
        {
            switch (echo)
            {
                case "@everyone":
                    await ReplyAsync("lol no");
                    break;
                case "@here":
                    await ReplyAsync("lol no");
                    break;
                default:
                    await ReplyAsync(echo);
                    break;
            }
        }

        [Command("square")]
        [Summary("Squares a number.")]
        public async Task SquareAsync([Summary("The number to square.")] string num)
        {
            float value = float.Parse(num, CultureInfo.InvariantCulture.NumberFormat);
            await Context.Channel.SendMessageAsync($"{value}^2 = {Math.Pow(value, 2)}");
        }

        [Command("userinfo")]
        [Summary("Returns info about the current user, or the user parameter, if one is passed")]
        public async Task UserInfoAsync([Summary("The (optional)")] SocketUser user)
        {
            SocketUser userInfo = user ?? Context.Client.CurrentUser;
            if (user != null)
                await ReplyAsync($"{user.Username}#{userInfo.Discriminator}");
        }

        [Command("version")]
        [Alias("vers", "ver", "v")]
        [Summary("Returns the current version of the bot")]
        public async Task Version()
        {
            await Context.Channel.SendMessageAsync($"Namoroka v{NamorokaV2.Version.ShortVersion} : Discord.Net v{NamorokaV2.Version.DiscordVersion}");
        }

        [Command("avatar")]
        [Summary("Gets the users avatar and sends it in the channel")]
        public async Task Avatar(SocketUser user)
        {
            SocketUser userInfo = user ?? Context.Client.CurrentUser;
            if (user != null)
                await ReplyAsync(userInfo.GetAvatarUrl());
        }

        [Command("bread")]
        public async Task Bread()
        {
            EmbedBuilder builder = new EmbedBuilder
            {
                Title = "here's the loaf",
                ImageUrl = "https://i.imgur.com/cenSuAk.png"
            };

            
            Embed embed = builder.Build();
            
            await Context.Channel.SendMessageAsync(null, false, embed);
        }

        [RequireRole(RoleIds.Administrator)]
        [Command("prefix")]
        public async Task PrefixAsync(string prefix)
        {
            string json = await File.ReadAllTextAsync(JsonService._configJson);
            dynamic jsonObj = JsonConvert.DeserializeObject(json);
            jsonObj["prefix"] = prefix;
            string output = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
            await File.WriteAllTextAsync(JsonService._configJson, output);
            
            await Context.Channel.SendMessageAsync($"I changed your prefix to: {prefix}");
        }
    }
}  