using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using NamorokaV2.Attributes;
using NamorokaV2.Configuration;
using NamorokaV2.NamorokaCore.Extensions;
using Victoria;
using Victoria.Enums;
using Victoria.EventArgs;
using Victoria.Responses.Rest;

namespace NamorokaV2.NamorokaCore.Modules.Audio
{
    [RequireContext(ContextType.Guild)]
    public sealed class AudioModule : ModuleBase<SocketCommandContext>
    {
        private readonly List<LavaTrack> _queueList;
        private readonly LavaNode _lavaNode;
        private readonly ConcurrentDictionary<ulong, CancellationToken> _disconnectTokens;
        
        public AudioModule(LavaNode lavaNode)
        {
            _lavaNode = lavaNode;
            _queueList = new List<LavaTrack>();
            _disconnectTokens = new ConcurrentDictionary<ulong, CancellationToken>();
        }
        
        [Command("queue")]
        [Summary("prints the full queue that is currently active")]
        [Remarks("-queue")]
        public async Task PrintQueueAsync() => await PrintQueueAsync(_lavaNode.GetPlayer(Context.Guild));
        
        [Command("skip")]
        [Summary("Skips the current track")]
        [Remarks("-skip")]
        [RequireUserPermission(GuildPermission.MoveMembers)]
        public async Task SkipCurrentTrackAsync() => await SkipTrackAsync(_lavaNode.GetPlayer(Context.Guild));
        
        // Will do for now 
        [Command("leave")]
        public async Task LeaveAsync() => await LeaveAsync(_lavaNode);

        [Command("play")]
        //[Summary("Plays a song with a link or song name")]
        //[Remarks("-play <song name/link>")]
        public async Task PlayAsync([Remainder] string searchQuery) => await PlaySongAsync(searchQuery);
        
        
        // ---------------------------- Audio Module Helpers ----------------------------
        
        private async Task PlaySongAsync(string searchQuery)
        {
            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                await ReplyAsync("Please provide search terms.");
                return;
            }

            await JoinAsync();
            await QueryAndPlayAsync(searchQuery);
            await Context.DeleteAuthorMessage();
            if (!_lavaNode.HasPlayer(Context.Guild))
            {
                await JoinAsync();
                await QueryAndPlayAsync(searchQuery);
            }
        }
        
        private async Task JoinAsync()
        {
            if (_lavaNode.HasPlayer(Context.Guild))
            {
                return;
            }

            var voiceState = Context.User as IVoiceState;
            if (voiceState?.VoiceChannel == null)
            {
                await Context.DeleteAuthorMessage();
                await ReplyAsync("You must be connected to a voice channel!");
                return;
            }

            try
            {
                await Context.DeleteAuthorMessage();
                await _lavaNode.JoinAsync(voiceState.VoiceChannel, Context.Channel as ITextChannel);
                await ReplyAsync($"Joined {voiceState.VoiceChannel.Name}");
            }
            catch (Exception exception)
            {
                await Context.DeleteAuthorMessage();
                await ReplyAsync(exception.Message);
            }
        }

        private async Task SkipTrackAsync(LavaPlayer track)
        {
            var player = _lavaNode.GetPlayer(Context.Guild).Track;
            var skipped = $"{player.Author} :: {player.Title} :: {player.Duration}";
            var builder = new EmbedBuilder()
                .AddField("Skipped", skipped);
            var embed = builder.Build();
            await ReplyAsync(embed: embed);
            await Context.DeleteAuthorMessage();
            await track.SkipAsync();
        }

        private async Task LeaveAsync(LavaNode lavaNode)
        {
            if (!lavaNode.HasPlayer(Context.Guild))
            {
                await ReplyAsync("I'm not connected to a voice channel.");
                return;
            }
            else
            {
                if (Context.User is IVoiceState voiceState)
                {
                    await lavaNode.LeaveAsync(voiceState.VoiceChannel);
                }   
            }
        }

        private async Task PrintQueueAsync(LavaPlayer player)
        {
            var builder = new EmbedBuilder();
            var stringBuilder = new StringBuilder();
            if (player.Queue.Count >= 1)
            {
                foreach (var item in player.Queue)
                {
                    stringBuilder.AppendLine($"{item.Author} :: {item.Title} :: {item.Duration}\n");
                }

                builder.AddField("------ Currently Playing ------\n",
                    $"{player.Track.Author} :: {player.Track.Title} :: {player.Track.Duration}");
                builder.AddField("------ Tracks ------\n", stringBuilder.ToString());
                var buildEmbed = builder.Build();
                await Context.DeleteAuthorMessage();
                await ReplyAsync(embed: buildEmbed);
            }
            else if (player.Queue.Count == 0)
            {
                builder = new EmbedBuilder().AddField("------ Currently Playing ------\n",
                    $"{player.Track.Author} :: {player.Track.Title} :: {player.Track.Duration}");
                var embed = builder.Build();
                await Context.DeleteAuthorMessage();
                await ReplyAsync(embed: embed);
            }
        }
        
        private async Task QueryAndPlayAsync(string searchQuery)
        {
            var queries = searchQuery.Split(' ');
            SearchResponse searchResponse;
            if (searchQuery.Contains('/') || searchQuery.Contains('?'))
            {
                foreach (var query in queries)
                {
                    searchResponse = await _lavaNode.SearchAsync(query);

                    if (searchResponse.LoadStatus == LoadStatus.LoadFailed || searchResponse.LoadStatus == LoadStatus.NoMatches)
                    {
                        return;
                    }
                    var player = _lavaNode.GetPlayer(Context.Guild);

                    if (player.PlayerState == PlayerState.Playing || player.PlayerState == PlayerState.Paused)
                    {
                        if (!string.IsNullOrWhiteSpace(searchResponse.Playlist.Name))
                        {
                            foreach (var track in searchResponse.Tracks)
                            {
                                player.Queue.Enqueue(track);
                            }

                            await ReplyAsync($"Enqueued {searchResponse.Tracks.Count} tracks.");
                        }
                        else
                        {
                            var track = searchResponse.Tracks[0];
                            
                            player.Queue.Enqueue(track);
                            
                            await ReplyAsync($"Enqueued: {track.Title}");
                        }
                    }
                    else
                    {
                        var track = searchResponse.Tracks[0];

                        if (!string.IsNullOrWhiteSpace(searchResponse.Playlist.Name))
                        {
                            for (var i = 0; i < searchResponse.Tracks.Count; i++)
                            {
                                if (i == 0)
                                {
                                    await player.PlayAsync(track);
                                    await ReplyAsync($"Now Playing: {track.Title}");
                                }
                                else
                                {
                                    player.Queue.Enqueue(searchResponse.Tracks[i]);
                                }
                            }

                            await ReplyAsync($"Enqueued {searchResponse.Tracks.Count} tracks.");
                        }
                        else
                        {
                            await player.PlayAsync(track);
                            await ReplyAsync($"Now Playing: {track.Title}");
                        }
                    }
                }
            }
            else
            {
                searchResponse = await _lavaNode.SearchYouTubeAsync(searchQuery);

                LavaTrack track;
                if (searchResponse.LoadStatus == LoadStatus.LoadFailed || searchResponse.LoadStatus == LoadStatus.NoMatches)
                {
                    return;
                }
                var player = _lavaNode.GetPlayer(Context.Guild);

                if (player.PlayerState == PlayerState.Playing || player.PlayerState == PlayerState.Paused)
                {
                    track = searchResponse.Tracks[0];
                        
                    player.Queue.Enqueue(track);
                    
                    await ReplyAsync($"Enqueued: {track.Title}");
                }
                else
                {
                    track = searchResponse.Tracks[0];
                    await player.PlayAsync(track);
                    await ReplyAsync($"Now Playing: {track.Title}");
                }
            }

            return;
        }


        // ---------------------------- Victoria Event Handlers ----------------------------
        
        private async Task OnTrackEnded(TrackEndedEventArgs args)
        {
            if (!args.Reason.ShouldPlayNext())
                return;

            var player = args.Player;
            if (!player.Queue.TryDequeue(out var queueable))
            {
                await player.TextChannel.SendMessageAsync("Queue completed! Please add more tracks to rock n' roll!");
                return;
            }

            if (!(queueable is { } track))
            {
                await player.TextChannel.SendMessageAsync("Next item in the queue is not a track.");
                return;
            }

            await args.Player.PlayAsync(track);
            await args.Player.TextChannel.SendMessageAsync($"{args.Reason}: {args.Track.Title}\nNow playing: {track.Title}");
        }
    }
}