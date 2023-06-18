using faxnocapBPbot.JsonModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace faxnocapBPbot.Helpers
{
    public static class BattleboardAggregator
    {
        public static BattleboardModel CombineBattleboards(string title, List<string> battleboards)
        {
            var serializedBattleboards = battleboards.Select(bb => JsonConvert.DeserializeObject<BattleboardModel>(bb)).ToList();
            BattleboardModel combinedBattleboard = new BattleboardModel
            {
                Title = title,
                StartTime = serializedBattleboards[0].StartTime,
                Id = String.Join(",", serializedBattleboards.Select(bb => bb.Id))
            };
            foreach (var bb in serializedBattleboards)
            {
                combinedBattleboard.TotalKills += bb.TotalKills;
                CombineData(bb.Players, combinedBattleboard.Players);
                CombineData(bb.Guilds, combinedBattleboard.Guilds);
                CombineData(bb.Alliances, combinedBattleboard.Alliances);
            }
            combinedBattleboard.TotalPlayers = combinedBattleboard.Players.Count;
            foreach (var player in combinedBattleboard.Players)
            {
                if(!string.IsNullOrEmpty(player.Value.AllianceId)) combinedBattleboard.Alliances[player.Value.AllianceId].Players += 1;
                if(!string.IsNullOrEmpty(player.Value.GuildId)) combinedBattleboard.Guilds[player.Value.GuildId].Players += 1;
            }
            return combinedBattleboard;
        }
        private static void CombineData<TKey, TValue>(Dictionary<TKey, TValue> source, Dictionary<TKey, TValue> destination)
            where TValue : ICommonData
        {
            foreach (TKey key in source.Keys)
            {
                if (destination != null && destination.ContainsKey(key))
                {
                    destination[key].KillFame += source[key].KillFame;
                    destination[key].Kills += source[key].Kills;
                    destination[key].Deaths += source[key].Deaths;
                }
                else
                {
                    destination.Add(key, source[key]);
                }
            }
        }
    }
}
