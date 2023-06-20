using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using faxnocapBPbot.Interfaces;
using System.Threading.Tasks;

namespace faxnocapBPbot.Commands
{
    public class GuildCommands : ApplicationCommandModule
    {
        private readonly IGuild _guild;
        public GuildCommands(IGuild guild)
        {
            _guild = guild;
        }

        [SlashCommand("FetchMembersStats", "Fetches members statistics.")]
        public async Task FetchMembersStats(InteractionContext ctx, [Option("season", "Enter id of season.")] string season)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent(Status.Working + "Posting members stats."));
            var task = await _guild.PostMembersStats(season);
            if (task.Item1) await ctx.Channel.SendMessageAsync(Status.Success + "Members stats successfully posted.").ConfigureAwait(false);
            else
            {
                await ctx.Channel.SendMessageAsync(Status.Failed + "Error during posting members stats.").ConfigureAwait(false);
                await ctx.Channel.SendMessageAsync(Status.Failed + task.Item2).ConfigureAwait(false);
            }
        }

        [SlashCommand("FetchGuildStats", "Fetches guild's statistics.")]
        public async Task FetchGuildStats(InteractionContext ctx, [Option("season", "Enter id of season.")] string season)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent(Status.Working + "Posting guild stats."));
            var task = await _guild.PostGuildStats(season);
            if (task.Item1) await ctx.Channel.SendMessageAsync(Status.Success + "Guild stats successfully posted.").ConfigureAwait(false);
            else
            {
                await ctx.Channel.SendMessageAsync(Status.Failed + "Error during posting guild stats.").ConfigureAwait(false);
                await ctx.Channel.SendMessageAsync(Status.Failed + task.Item2).ConfigureAwait(false);
            }
        }
    }
}
