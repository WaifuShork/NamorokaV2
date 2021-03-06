﻿using Discord;
using Discord.Commands;
using System.Linq;
using System.Threading.Tasks;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using NamorokaV2.Configuration;
using NamorokaV2.NamorokaCore.Extensions;
using NamorokaV2.NamorokaCore.Services;

namespace NamorokaV2.NamorokaCore.Modules.UtilityModules
{
    public class HelpModule : ModuleBase<SocketCommandContext>
    {
        private readonly CommandService _service;
        private readonly IConfigurationRoot _config;
        public HelpModule(CommandService service, IConfigurationRoot config)
        {
            _service = service;
            _config = config;
        }

        [Command("help")]
        public async Task HelpAsync()
        {
            await Context.DeleteAuthorMessage();
            var prefix = _config["prefix"];
            EmbedBuilder builder = new EmbedBuilder()
            {
                Color = new Color(114, 137, 218),
                Description = "These are the commands you can use"
            };
            
            foreach (ModuleInfo module in _service.Modules)
            {
                string description = null;
                foreach (CommandInfo cmd in module.Commands)
                {
                    PreconditionResult result = await cmd.CheckPreconditionsAsync(Context);
                    if (result.IsSuccess)
                        description += $"{prefix}{cmd.Aliases.First()}\n";
                }
                
                if (!string.IsNullOrWhiteSpace(description))
                {
                    builder.AddField(x =>
                    {
                        x.Name = module.Name;
                        x.Value = description;
                        x.IsInline = false;
                    });
                }
            }

            await ReplyAsync(null, false, builder.Build());
        }

        [Command("help")]
        public async Task HelpAsync(string command)
        {
            SearchResult result = _service.Search(Context, command);

            await Context.DeleteAuthorMessage();
            if (!result.IsSuccess)
            {
                await ReplyAsync($"Sorry, I couldn't find a command like **{command}**.");
                return;
            }

            EmbedBuilder builder = new EmbedBuilder()
            {
                Color = new Color(114, 137, 218),
                Description = $"Here are some commands like **{command}**"
            };

            foreach (CommandMatch match in result.Commands)
            {
                CommandInfo cmd = match.Command;

                builder.AddField(x =>
                {
                    x.Name = string.Join(", ", cmd.Aliases);
                    x.Value = $"Parameters: {string.Join(", ", cmd.Parameters.Select(p => p.Name))}\n" + 
                              $"Summary: {cmd.Summary}";
                    x.IsInline = false;
                });
            }

            await ReplyAsync(null, false, builder.Build());
        }
    }
}