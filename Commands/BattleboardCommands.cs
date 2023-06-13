using DSharpPlus.SlashCommands;
using faxnocapBPbot.Handlers;
using System;
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
            Battleboard bb = new Battleboard();

            await ctx.Channel.SendMessageAsync(Status.Working + "Posting battleboard.").ConfigureAwait(false);
            var arrId = Array.ConvertAll(input.Split(','), int.Parse);
            var task = await bb.PostBattleboard(content, season, title, arrId);
            if (task.Item1) await ctx.Channel.SendMessageAsync(Status.Success + "Battleboard successfully posted.").ConfigureAwait(false);
            else
            {
                await ctx.Channel.SendMessageAsync(Status.Failed + "Error during posting battleboard.").ConfigureAwait(false);
                await ctx.Channel.SendMessageAsync(Status.Failed + task.Item2).ConfigureAwait(false); // ex.message
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
