using DSharpPlus.SlashCommands;
using System.Threading.Tasks;

namespace faxnocapBPbot.Commands
{
    public class GuildCommands : ApplicationCommandModule
    {
        [SlashCommand("FetchMembersStats", "Fetches members statistics.")]
        public async Task FetchMembersStats(InteractionContext ctx)
        {
            await ctx.Channel.SendMessageAsync("test").ConfigureAwait(false);
        }

        [SlashCommand("FetchGuildStats", "Fetches guild's statistics.")]
        public async Task FetchGuildStats(InteractionContext ctx)
        {

        }
    }
}
