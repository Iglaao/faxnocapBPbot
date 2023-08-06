using faxnocapBPbot.Helpers;
using faxnocapBPbot.Interfaces;
using faxnocapBPbot.JsonModels;
using Google.Cloud.Firestore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace faxnocapBPbot.Handlers
{
    public class Battleboard : IBattleboard
    {
        private DocumentReference _docRef;
        private DocumentSnapshot _snapshot;
        public async Task<(bool, string)> PostBattleboard(string season, string title, params int[] battleId)
        {
            try
            {
                var battleboards = await Task.WhenAll(battleId.Select(id => AlbionApiFetcher.FetchAPI(AlbionApiFetcher.BattleUrl + id)));
                var combinedBattleboard = BattleboardAggregator.CombineBattleboards(title, battleboards.ToList());
                
                await PostBattleboardData(season, combinedBattleboard);
                await PostBattleboardMap(season, combinedBattleboard);
                await UpdatePlayers(season, combinedBattleboard, true);
                
                return (true, "");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
        private async Task PostBattleboardData(string season, BattleboardModel battleboard)
        {
            var bbWrapper = new Dictionary<string, object>
            {
                { battleboard.Id, battleboard }
            };
            _docRef = Firestore.GetInstance().Collection(season).Document(battleboard.StartTime.ToString("dd.MM.yyyy"));
            await _docRef.SetAsync(bbWrapper, SetOptions.MergeAll);
        }
        private async Task PostBattleboardMap(string season, BattleboardModel battleboard)
        {
            var dict = new Dictionary<string, object>
            {
                { battleboard.Id, new Dictionary<string, object>
                    {
                        { "Title", battleboard.Title },
                        { "StartTime", battleboard.StartTime },
                    }
                }
            };
            _docRef = Firestore.GetInstance().Collection(season + "dict").Document("dict");
            await _docRef.SetAsync(dict, SetOptions.MergeAll);
        }
        public async Task<(bool, string)> RemoveBattleboard(string season, string date, string id)
        {
            try
            {
                _docRef = Firestore.GetInstance().Collection(season).Document(date);
                _snapshot = await _docRef.GetSnapshotAsync();
                if (_snapshot.TryGetValue<BattleboardModel>(id, out var battleboard))
                {
                    await DeleteBattleboardData(_docRef, season, id);
                    await UpdatePlayers(season, battleboard, false);
                    return (true, "");
                }
                return (false, "Battleboard not found.");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
        private async Task DeleteBattleboardData(DocumentReference docRef, string season, string id)
        {
            var bbToDelete = new Dictionary<string, object>
            {
                { id, FieldValue.Delete }
            };
            await docRef.UpdateAsync(bbToDelete);

            _docRef = Firestore.GetInstance().Collection(season + "dict").Document("dict");
            await _docRef.UpdateAsync(bbToDelete);
        }
        private async Task UpdatePlayers(string season, BattleboardModel battleboard, bool update)
        {
            int action = update ? 1 : -1;
            string battleTime = battleboard.StartTime.ToString("dd.MM.yyyy");

            var guildPlayers = battleboard.Players.Where(p => p.Value.GuildName.Equals("Fax")).ToList();

            foreach(var player in guildPlayers)
            {
                await UpdatePlayerData(season, battleTime, player.Value, action);
            }
            await UpdatePlayerAttendance(season, battleTime, guildPlayers, update);
        }
        private async Task UpdatePlayerData(string season, string battleTime, BattleboardPlayerModel playerData, int action)
        {
            _docRef = Firestore.GetInstance().Collection(season + "members").Document(playerData.Name);
            _snapshot = await _docRef.GetSnapshotAsync();

            var existingPlayerData = _snapshot.Exists ? _snapshot.ToDictionary() : new Dictionary<string, object>();
            var dateObj = existingPlayerData.TryGetValue(battleTime, out var obj) ? obj as Dictionary<string, object> : null;
            
            if (dateObj is null)
            {
                dateObj = new Dictionary<string, object>();
            }

            dateObj["Kills"] = FieldValue.Increment(action * playerData.Kills);
            dateObj["Deaths"] = FieldValue.Increment(action * playerData.Deaths);
            dateObj["Attendance"] = FieldValue.Increment(action);

            existingPlayerData[battleTime] = dateObj;

            await _docRef.SetAsync(existingPlayerData, SetOptions.MergeAll);
        }
        private async Task UpdatePlayerAttendance(string season, string battleTime, List<KeyValuePair<string, BattleboardPlayerModel>> players , bool update)
        {
            int action = update ? 1 : -1;
            AttendanceModel attendance = null;
            _docRef = Firestore.GetInstance().Collection(season + "dict").Document("attendance");
            _snapshot = await _docRef.GetSnapshotAsync();

            var attendanceDoc = _snapshot.Exists ? _snapshot.ToDictionary() : new Dictionary<string, object>();
            var data = attendanceDoc.TryGetValue("attendanceData", out var obj) ? obj as StringBuilder : null;
            if (data == null) attendance = new AttendanceModel() { Date = new Dictionary<string, AttendanceDateModel>()};
            else
            {
                attendance = JsonConvert.DeserializeObject<AttendanceModel>(data.ToString());
            }

            if(!attendance.Date.TryGetValue(battleTime, out var attendanceDate))
            {
                attendanceDate = new AttendanceDateModel { PlayersAttendance = new Dictionary<string, PlayerAttendanceStatsModel>() };
                attendance.Date[battleTime] = attendanceDate;
            }

            foreach(var player in players)
            {
                var playerName = player.Key;
                var playerData = player.Value;

                if (!attendanceDate.PlayersAttendance.TryGetValue(playerName, out var playerStats))
                {
                    playerStats = new PlayerAttendanceStatsModel();
                    attendanceDate.PlayersAttendance[playerData.Name] = playerStats;
                }

                playerStats.Attendance += action;
                playerStats.Kills += action * playerData.Kills;
                playerStats.Deaths += action * playerData.Deaths;
            }

            var updatedData = JsonConvert.SerializeObject(attendance);
            attendanceDoc["attendanceData"] = updatedData;
            await _docRef.SetAsync(attendanceDoc, SetOptions.MergeAll);
        }
    }
}
