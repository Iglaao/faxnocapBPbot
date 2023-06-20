using DSharpPlus;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.SlashCommands;
using faxnocapBPbot.Commands;
using faxnocapBPbot.ConfigStructs;
using faxnocapBPbot.Handlers;
using faxnocapBPbot.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace faxnocapBPbot
{
    public class Bot
    {
        public DiscordClient Client { get; private set; }
        public InteractivityExtension Interactivity { get; private set; }
        
        public async Task RunAsync()
        {
            var configJson = ConfigDeserializator.ReturnDeserializedJson<DiscordConfig>("config.json");

            var config = new DiscordConfiguration()
            {
                Intents = DiscordIntents.All,
                Token = configJson.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
            };

            Client = new DiscordClient(config);
            Client.Ready += OnClientReady;
            Client.UseInteractivity(new InteractivityConfiguration()
            {
                Timeout = TimeSpan.FromMinutes(2)
            });

            var slashCommandsConfig = Client.UseSlashCommands(new SlashCommandsConfiguration
            {
                Services = new ServiceCollection()
                    .AddSingleton<IBattleboard, Battleboard>()
                    .AddSingleton<IGuild, Guild>()
                    .BuildServiceProvider()
            });
            slashCommandsConfig.RegisterCommands<GuildCommands>();
            slashCommandsConfig.RegisterCommands<BattleboardCommands>();

            await Client.ConnectAsync();
            await Task.Delay(-1);
        }

        private Task OnClientReady(DiscordClient client, ReadyEventArgs e)
        {
            return Task.CompletedTask;
        }
    }
}
