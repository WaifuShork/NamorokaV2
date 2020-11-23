using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using NamorokaV2.Attributes;
using Newtonsoft.Json;

namespace NamorokaV2  
{
    public class FizzBuzzModule : ModuleBase<SocketCommandContext>
    {
        [Command("fb")]
        [Summary("FizzBuzz algorithm.")]
        public async Task FizzBuzzAsync(int count)
        {
            string append = default;
            if (count >= 51)
            {
                await Context.Channel.SendMessageAsync("Please input a number from at least 1 to 50, as to reduce spam.");
                return;
            }
            for (int i = 1; i <= count; i++)
            {
                switch (i % 3)
                {
                    case 0 when i % 5 == 0:
                        append += "FizzBuzz\n";
                        break;
                    case 0:
                        append += "Fizz\n";
                        break;
                    default:
                    {
                        if (i % 5 == 0)
                            append += "Buzz\n";
                        else
                            append += $"{i}\n";
                        break;
                    }
                }
            }
            await Context.Channel.SendMessageAsync($"```FizzBuzz Results:\n{append}```");
        }
    }
}