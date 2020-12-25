﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
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
            var sb = new StringBuilder();
            if (count >= 51)
            {
                await Context.Channel.SendMessageAsync("Please input a number from at least 1 to 50, as to reduce spam.");
                return;
            }
            
            for (var i = 1; i <= count; i++)
            {
                switch (i % 3)
                {
                    case 0 when i % 5 == 0:
                        sb.AppendLine("FizzBuzz");
                        break;
                    case 0:
                        sb.AppendLine("Fizz");
                        break;
                    default:
                    {
                        if (i % 5 == 0)
                            sb.AppendLine("Buzz");
                        else
                            sb.AppendLine($"{i}");
                        break;
                    }
                }
            }
            await Context.Channel.SendMessageAsync($"```FizzBuzz Results:\n{sb}```");
        }
    }
}