using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;

namespace faxnocapBPbot.JsonModels
{
    [FirestoreData]
    public class BattleboardModel
    {
        public BattleboardModel()
        {
            Players = new Dictionary<string, BattleboardPlayerModel>();
            Guilds = new Dictionary<string, BattleboardGuildModel>();
            Alliances = new Dictionary<string, BattleboardAllianceModel>();
        }

        [FirestoreProperty]
        public string Id { get; set; }
        [FirestoreProperty]
        public string Title { get; set; }
        [FirestoreProperty]
        public DateTime StartTime { get; set; }
        [FirestoreProperty]
        public int TotalKills { get; set; }
        [FirestoreProperty]
        public int TotalPlayers { get; set; }
        [FirestoreProperty]
        public Dictionary<string, BattleboardPlayerModel> Players { get; set; }
        [FirestoreProperty]
        public Dictionary<string, BattleboardGuildModel> Guilds { get; set; }
        [FirestoreProperty]
        public Dictionary<string, BattleboardAllianceModel> Alliances { get; set; }
    }
    [FirestoreData]
    public class BattleboardPlayerModel : ICommonData
    {
        [FirestoreProperty]
        public string Name { get; set; }
        [FirestoreProperty]
        public int Kills { get; set; }
        [FirestoreProperty]
        public int Deaths { get; set; }
        [FirestoreProperty]
        public ulong KillFame { get; set; }
        [FirestoreProperty]
        public string GuildName { get; set; }
        [FirestoreProperty]
        public string GuildId { get; set; }
        [FirestoreProperty]
        public string AllianceName { get; set; }
        [FirestoreProperty]
        public string AllianceId { get; set; }
    }
    [FirestoreData]
    public class BattleboardGuildModel : ICommonData
    {
        [FirestoreProperty]
        public string Name { get; set; }
        [FirestoreProperty]
        public int Players { get; set; }
        [FirestoreProperty]
        public int Kills { get; set; }
        [FirestoreProperty]
        public int Deaths { get; set; }
        [FirestoreProperty]
        public ulong KillFame { get; set; }
        [FirestoreProperty]
        public string Alliance { get; set; }
        [FirestoreProperty]
        public string AllianceId { get; set; }
    }
    [FirestoreData]
    public class BattleboardAllianceModel : ICommonData
    {
        [FirestoreProperty]
        public string Name { get; set; }
        [FirestoreProperty]
        public int Players { get; set; }
        [FirestoreProperty]
        public int Kills { get; set; }
        [FirestoreProperty]
        public int Deaths { get; set; }
        [FirestoreProperty]
        public ulong KillFame { get; set; }
    }
    public interface ICommonData
    {
        ulong KillFame { get; set; }
        int Kills { get; set; }
        int Deaths { get; set; }
    }
}