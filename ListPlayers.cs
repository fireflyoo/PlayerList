using System;
using System.Collections.Generic;
using System.Linq;
using TShockAPI;
using Terraria;
using TerrariaApi.Server;

namespace GetOfflinePlayersHealthPlugin
{
    [ApiVersion(2, 1)]
    public class Plugin : TerrariaPlugin
    {
        public override string Name => "Get Offline Players Health Plugin";
        public override string Author => "fireflyoo";
        public override string Description => "A plugin to get health of all offline players";
        public override Version Version => new Version(1, 0);

        public Plugin(Main game) : base(game)
        {
            Order = 1;
        }

        public override void Initialize()
        {
            Commands.ChatCommands.Add(new Command("list.health", GetOfflinePlayersHealth, "ls"));
        }

        private void GetOfflinePlayersHealth(CommandArgs args)
        {
            var offlinePlayersHealth = new List<(string Name, int Health, int MaxHealth)>();

            // Iterate through all known user accounts
            foreach (var userAccount in TShock.UserAccounts.GetUserAccounts())
            {


                // Load the player's data
                var playerData = TShock.CharacterDB.GetPlayerData(args.Player,userAccount.ID);
                
                // Add player's health information to the list
                if (playerData != null)
                {
                    offlinePlayersHealth.Add((userAccount.Name, playerData.health, playerData.maxHealth));
                }
            }

            // Display the health information
            foreach (var player in offlinePlayersHealth)
            {
                args.Player.SendSuccessMessage($"Player: {player.Name}, Health: {player.Health}/{player.MaxHealth}");
            }
        }
    }
}