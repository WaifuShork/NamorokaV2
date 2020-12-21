using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using NamorokaV2.Attributes;
using Newtonsoft.Json;
using Discord.Addons.Interactive;

namespace NamorokaV2
{
    public sealed class InfoModule : ModuleBase<SocketCommandContext>
    {
        [Command("say")]
        [Summary("Echoes a message.")]
        public async Task SayAsync([Remainder] [Summary("The text to echo")] string echo)
        {

            SocketUserMessage message = Context.Message;
            switch (echo)
            {
                case "@everyone":
                    await Context.Channel.DeleteMessageAsync(message);
                    await ReplyAsync("lol no");
                    break;
                case "@here":
                    await Context.Channel.DeleteMessageAsync(message);
                    await ReplyAsync("lol no");
                    break;
                default:
                    await Context.Channel.DeleteMessageAsync(message);
                    await ReplyAsync(echo);
                    break;
            }
        }
        
        [Command("square")]
        [Summary("Squares a number.")]
        public async Task SquareAsync([Summary("The number to square.")] string num)
        {
            SocketUserMessage message = Context.Message;
            await Context.Channel.DeleteMessageAsync(message);
            float value = float.Parse(num, CultureInfo.InvariantCulture.NumberFormat);
            await Context.Channel.SendMessageAsync($"{value}^2 = {Math.Pow(value, 2)}");
        }
        
        [Command("f")]
        public async Task FAsync()
        {
            SocketUserMessage message = Context.Message;
            await Context.Channel.DeleteMessageAsync(message);
            await ReplyAsync("F in chat bois");
        }

        [Command("version")]
        [Alias("vers", "ver", "v")]
        [Summary("Returns the current version of the bot")]
        public async Task Version()
        {
            SocketUserMessage message = Context.Message;
            await Context.Channel.DeleteMessageAsync(message);
            await Context.Channel.SendMessageAsync($"Namoroka v{NamorokaV2.Version.ShortVersion} : Discord.Net v{NamorokaV2.Version.DiscordVersion}");
        }

        [Command("avatar")]
        [Summary("Gets the users avatar and sends it in the channel")]
        public async Task Avatar(SocketUser user)
        {
            SocketUserMessage message = Context.Message;
            await Context.Channel.DeleteMessageAsync(message);
            SocketUser userInfo = user ?? Context.Client.CurrentUser;
            if (user != null)
                await ReplyAsync(userInfo.GetAvatarUrl());
        }

        [Command("bread")]
        public async Task Bread()
        {
            SocketUserMessage message = Context.Message;
            await Context.Channel.DeleteMessageAsync(message);
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
            SocketUserMessage message = Context.Message;
            await Context.Channel.DeleteMessageAsync(message);
            string json = await File.ReadAllTextAsync(JsonService._configJson);
            dynamic jsonObj = JsonConvert.DeserializeObject(json);
            jsonObj["prefix"] = prefix;
            string output = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
            await File.WriteAllTextAsync(JsonService._configJson, output);
            
            await Context.Channel.SendMessageAsync($"I changed your prefix to: {prefix}");
        }

        #pragma warning disable 1998
        public async Task OnUserJoin()
        #pragma warning restore 1998
        {
            SocketRole role = Context.Guild.GetRole(RoleIds.MakoShork);
            Context.Client.UserJoined += user => user.AddRoleAsync(role);
        }
        
        [Command("cheesymac")]
        public async Task CheesyMacAsync()
        {
            SocketUserMessage message = Context.Message;
            await Context.Channel.DeleteMessageAsync(message);
            await Context.Channel.SendMessageAsync("I'M SORRY MY PUSSY IS SO TIGHT AND JUICY AND SOUNDS LIKE MACARONI & CHEESE IT'S NOT MY FAULT <@329502393672663042>");
        }
        
        [Command("zoop")]
        public async Task ZoopAsync()
        {
            SocketUserMessage message = Context.Message;
            await Context.Channel.DeleteMessageAsync(message);
            await ReplyAsync("This is");
            await Task.Delay(2000);
            await ReplyAsync("a test");
        }
    }
}  