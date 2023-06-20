using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using faxnocapBPbot.Interfaces;
using System;
using System.Threading.Tasks;

namespace faxnocapBPbot.Commands
{
    public class BattleboardCommands : ApplicationCommandModule
    {
        private readonly IBattleboard _battleboard;
        public BattleboardCommands(IBattleboard battleboard)
        {
            _battleboard = battleboard;
        }

        [SlashCommand("PostBattleBoard", "Post battleboard.")]
        public async Task PostBattleBoard(InteractionContext ctx,
            [Option("season", "Enter id of season.")] string season,
            [Option("name", "Enter name of battleboard.")] string title,
            [Option("id", "Enter id of battleboard.")] string input)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent(Status.Working + "Posting battleboard."));
            var arrId = Array.ConvertAll(input.Split(','), int.Parse);
            var task = await _battleboard.PostBattleboard(season, title, arrId);
            if (task.Item1) await ctx.Channel.SendMessageAsync(Status.Success + "Battleboard successfully posted.").ConfigureAwait(false);
            else
            {
                await ctx.Channel.SendMessageAsync(Status.Failed + "Error during posting battleboard.").ConfigureAwait(false);
                await ctx.Channel.SendMessageAsync(Status.Failed + task.Item2).ConfigureAwait(false);
            }
        }

        [SlashCommand("RemoveBattleBoard", "Remove battleboard.")]
        public async Task RemoveBattleBoard(InteractionContext ctx,
            [Option("season", "Enter id of season.")] string season,
            [Option("date", "Enter date of battle.")] string date,
            [Option("id", "Type id of battleboard.")] string input)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent(Status.Working + "Removing battleboard."));
            var task = await _battleboard.RemoveBattleboard(season, date, input);
            if (task.Item1) await ctx.Channel.SendMessageAsync(Status.Success + "Battleboard successfully removed.").ConfigureAwait(false);
            else
            {
                await ctx.Channel.SendMessageAsync(Status.Failed + "Error during posting battleboard.").ConfigureAwait(false);
                await ctx.Channel.SendMessageAsync(Status.Failed + task.Item2).ConfigureAwait(false);
            }
        }
    }
}
