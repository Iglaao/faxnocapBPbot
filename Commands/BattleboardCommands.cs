using DSharpPlus.SlashCommands;
using System.Threading.Tasks;

namespace faxnocapBPbot.Commands
{
    public class BattleboardCommands : ApplicationCommandModule
    {
        [SlashCommand("PostBattleBoard", "Post battleboard.")]
        public async Task PostBattleBoard(InteractionContext ctx,
            [Option("content", "Type of content.")] string content,
            [Option("season", "Enter id of season.")] string season,
            [Option("name", "Enter name of battleboard.")] string title,
            [Option("id", "Enter id of battleboard.")] string input)
        {
            await ctx.Channel.SendMessageAsync(Status.Working + "Posting battleboard.").ConfigureAwait(false);
            //var t1 = Array.ConvertAll(test.Split(','), int.Parse);
            // PostBattleboard(string content, string season, string title, params int[] battleId)
            if (true) await ctx.Channel.SendMessageAsync(Status.Success + "Battleboard successfully posted.").ConfigureAwait(false);
            else
            {
                await ctx.Channel.SendMessageAsync(Status.Failed + "Error during posting battleboard.").ConfigureAwait(false);
                await ctx.Channel.SendMessageAsync(Status.Failed).ConfigureAwait(false); // ex.message
            }
        }

        [SlashCommand("RemoveBattleBoard", "Remove battleboard.")]
        public async Task RemoveBattleBoard(InteractionContext ctx, [Option("id", "Type id of battleboard.")] string input)
        {
            await ctx.Channel.SendMessageAsync(Status.Working).ConfigureAwait(false);
        }

        [SlashCommand("UpdateBattleBoard", "Update battleboard.")]
        public async Task UpdateBattleBoard(InteractionContext ctx, [Option("id", "Type id of battleboard.")] string input)
        {
            await ctx.Channel.SendMessageAsync(Status.Failed).ConfigureAwait(false);
        }
    }
}
