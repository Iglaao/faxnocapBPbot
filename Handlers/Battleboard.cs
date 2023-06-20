using faxnocapBPbot.Helpers;
using faxnocapBPbot.Interfaces;
using faxnocapBPbot.JsonModels;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace faxnocapBPbot.Handlers
{
    public class Battleboard : IBattleboard
    {
        private DocumentReference _docRef;
        private DocumentSnapshot _snapshot;
        public async Task<(bool, string)> PostBattleboard(string season, string title, params int[] battleId)
        {
            List<string> battleboards = new List<string>();
            try
            {
                foreach (int id in battleId) battleboards.Add(await AlbionApiFetcher.FetchAPI(AlbionApiFetcher.BattleUrl + id));
                var battleboard = BattleboardAggregator.CombineBattleboards(title, battleboards);
                //post bb
                _docRef = Firestore.GetInstance().Collection(season).Document(battleboard.StartTime.ToString("dd.MM.yyyy"));
                Dictionary<string, object> bbWrapper = new Dictionary<string, object>
                {
                    { battleboard.Id, battleboard },
                };
                await _docRef.SetAsync(bbWrapper, SetOptions.MergeAll);
                //post bb map
                _docRef = Firestore.GetInstance().Collection(season + "dict").Document("dict");
                Dictionary<string, object> dict = new Dictionary<string, object>
                {
                    {battleboard.Id, (battleboard.Title, battleboard.StartTime) }
                };
                await _docRef.SetAsync(dict, SetOptions.MergeAll);
                //update players
                await UpdatePlayer(season, battleboard, true);
                return (true, "");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
        public async Task<(bool, string)> RemoveBattleboard(string season, string date, string id)
        {
            try
            {
                _docRef = Firestore.GetInstance().Collection(season).Document(date);
                _snapshot = await _docRef.GetSnapshotAsync();
                BattleboardModel battleboard;
                _snapshot.TryGetValue<BattleboardModel>(id, out battleboard);
                Dictionary<string, object> bbToDelete = new Dictionary<string, object>
                {
                    { id, FieldValue.Delete }
                };
                await _docRef.UpdateAsync(bbToDelete);
                _docRef = Firestore.GetInstance().Collection(season + "dict").Document("dict");
                await _docRef.UpdateAsync(bbToDelete);
                await UpdatePlayer(season, battleboard, false);
                return (true, "");
            }
            catch(Exception ex)
            {
                return (false, ex.Message);
            }
        }
        private async Task UpdatePlayer(string season, BattleboardModel battleboard, bool update)
        {
            _docRef = null;
            _snapshot = null;
            Dictionary<string, object> playerData = null;
            int action = update ? 1 : -1;
            string battleTime = battleboard.StartTime.ToString("dd.MM.yyyy");
            var faxPlayers = battleboard.Players.Where(p => p.Value.GuildName.Equals("Fax"));
            foreach (var player in faxPlayers)
            {
                _docRef = Firestore.GetInstance().Collection(season + "members").Document(player.Value.Name);
                _snapshot = await _docRef.GetSnapshotAsync();
                if (_snapshot.Exists)
                {
                    playerData = _snapshot.ToDictionary();
                    if (playerData.TryGetValue(battleTime, out var dateObj) && dateObj is Dictionary<string, object> date)
                    {
                        date["Kills"] = FieldValue.Increment(action * player.Value.Kills);
                        date["Deaths"] = FieldValue.Increment(action * player.Value.Deaths);
                        date["Attendance"] = FieldValue.Increment(action * 1);
                    }
                    else
                    {
                        playerData[battleTime] = new Dictionary<string, object>
                        {
                            { "Kills", FieldValue.Increment(player.Value.Kills) },
                            { "Deaths", FieldValue.Increment(player.Value.Deaths) },
                            { "Attendance", FieldValue.Increment(1) }
                        };
                    }
                }
                else
                {
                    playerData = new Dictionary<string, object>
                    {
                        { battleTime, new Dictionary<string, object>
                            {
                                { "Kills", FieldValue.Increment(player.Value.Kills) },
                                { "Deaths", FieldValue.Increment(player.Value.Deaths) },
                                { "Attendance", FieldValue.Increment(1) }
                            }
                        }
                    };
                }
                await _docRef.SetAsync(playerData, SetOptions.MergeAll);
            }
        }
    }
}
