using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Victoria;
using Victoria.Enums;
using Victoria.EventArgs;

// Bot restarts nuke the validation of being in a voice channel. 
// Not entirely sure how to fix this 

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
        
        // TODO: Double check if this works
        [Command("queue")]
        [Summary("prints the full queue that is currently active")]
        [Remarks("-queue")]
        public async Task PrintQueueAsync() => await PrintQueueAsync(_lavaNode.GetPlayer(Context.Guild));
        
        [Command("skip")]
        [Summary("Skips the current track")]
        [Remarks("-skip")]
        public async Task SkipCurrentTrackAsync() => await SkipTrackAsync(_lavaNode.GetPlayer(Context.Guild));
        
        // Will do for now 
        [Command("leave")]
        public async Task LeaveAsync() => await LeaveAsync(_lavaNode);

        [Command("play")]
        [Summary("Plays a song with a link or song name")]
        [Remarks("-play <song name/link>")]
        public async Task PlayAsync([Remainder] string searchQuery) => await PlaySongAsync(searchQuery);

        
        // ---------------------------- Audio Module Helpers ----------------------------
        
        private async Task PlaySongAsync(string searchQuery)
        {
            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                await ReplyAsync("Please provide search terms.");
                return;
            }

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
                await ReplyAsync("I'm already connected to a voice channel!");
                return;
            }

            var voiceState = Context.User as IVoiceState;
            if (voiceState?.VoiceChannel == null)
            {
                await ReplyAsync("You must be connected to a voice channel!");
                return;
            }

            try
            {
                await _lavaNode.JoinAsync(voiceState.VoiceChannel, Context.Channel as ITextChannel);

                await ReplyAsync($"Joined {voiceState.VoiceChannel.Name}");
            }
            catch (Exception exception)
            {
                await ReplyAsync(exception.Message);
            }
        }
        
        private static async Task SkipTrackAsync(LavaPlayer track) => await track.SkipAsync();
        
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
            var stringBuilder = new StringBuilder();
            foreach (var item in player.Queue)
            {
                stringBuilder.AppendLine($"{item.Author} :: {item.Title} :: {item.Duration}" );
            }
            
            var builder = new EmbedBuilder()
                .AddField("------ Tracks ------\n",stringBuilder.ToString());
            var embed = builder.Build();
            
            await ReplyAsync(embed: embed);
        }
        
        private void RemoveDuplicates(LavaPlayer player)
        {
            foreach (var t in player.Queue)
            {
                _queueList.Add(t);
            }

            var lavaList = _queueList.Distinct().ToList();
            player.Queue.Clear();
            foreach (var t in lavaList)
            {
                player.Queue.Enqueue(t);
            }

            // Refresh queue list for next search
            _queueList.Clear();
        }
        
        private async Task QueryAndPlayAsync(string searchQuery)
        {
            var queries = searchQuery.Split(' ');
            foreach (var query in queries)
            {
                var searchResponse = await _lavaNode.SearchAsync(query);

                // TODO: Make sure duplicates are nuked :: It should be working now but just keep an eye
                if (searchResponse.LoadStatus == LoadStatus.LoadFailed || searchResponse.LoadStatus == LoadStatus.NoMatches)
                {
                    searchResponse = await _lavaNode.SearchYouTubeAsync(searchQuery);
                    if (searchResponse.LoadStatus == LoadStatus.LoadFailed || searchResponse.LoadStatus == LoadStatus.NoMatches)
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
                        RemoveDuplicates(player);

                        await ReplyAsync($"Enqueued {searchResponse.Tracks.Count} tracks.");
                    }
                    else
                    {
                        var track = searchResponse.Tracks[0];
                        player.Queue.Enqueue(track);
                        // TODO : Test later 
                        RemoveDuplicates(player);
                        
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
                                RemoveDuplicates(player);
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
            return;
        }
        
        
        // ---------------------------- Victoria Event Handlers ----------------------------
        
        // TODO: Does this work? 
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