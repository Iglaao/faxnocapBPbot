﻿using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using faxnocapBPbot.Handlers;
using faxnocapBPbot.Interfaces;
using System;
using System.Threading.Tasks;

namespace faxnocapBPbot.Commands
{
    public class BattleboardCommands : ApplicationCommandModule
    {
        private Battleboard _battleboard = new Battleboard();

        [SlashCommand("PostBattleBoard", "Post battleboard.")]
        public async Task PostBattleBoard(InteractionContext ctx,
            [Option("season", "Enter id of season.")] string season,
            [Option("content", "Type of content.")] string content,
            [Option("name", "Enter name of battleboard.")] string title,
            [Option("id", "Enter id of battleboard.")] string input)
        {
            Battleboard bb = new Battleboard();
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent(Status.Working + "Posting battleboard."));
            var arrId = Array.ConvertAll(input.Split(','), int.Parse);
            var task = await _battleboard.PostBattleboard(season, content, title, arrId);
            if (task.Item1) await ctx.Channel.SendMessageAsync(Status.Success + "Battleboard successfully posted.").ConfigureAwait(false);
            else
            {
                await ctx.Channel.SendMessageAsync(Status.Failed + "Error during posting battleboard.").ConfigureAwait(false);
                await ctx.Channel.SendMessageAsync(Status.Failed + task.Item2).ConfigureAwait(false);
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
