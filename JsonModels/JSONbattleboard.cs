using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace faxnocapBPbot.JsonModels
{
    [FirestoreData]
    public class JSONbattleboard
    {
        [FirestoreProperty]
        public ulong id { get; set; }
        [FirestoreProperty]
        public DateTime startTime { get; set; }
        [FirestoreProperty]
        public Dictionary<string, JSONbattleboardPlayer> players { get; set; }
        [FirestoreProperty]
        public Dictionary<string, JSONbattleboardGuild> guilds { get; set; }
        [FirestoreProperty]
        public Dictionary<string, JSONbattleboardAlliances> alliances { get; set; }
    }
    [FirestoreData]
    public class JSONbattleboardPlayer
    {
        [FirestoreProperty]
        public string name { get; set; }
        [FirestoreProperty]
        public int kills { get; set; }
        [FirestoreProperty]
        public int deaths { get; set; }
        [FirestoreProperty]
        public ulong killfame { get; set; }
        [FirestoreProperty]
        public string guildName { get; set; }
        [FirestoreProperty]
        public string allianceName { get; set; }
    }
    [FirestoreData]
    public class JSONbattleboardGuild
    {
        [FirestoreProperty]
        public string name { get; set; }
        [FirestoreProperty]
        public int kills { get; set; }
        [FirestoreProperty]
        public int deaths { get; set; }
        [FirestoreProperty]
        public ulong killFame { get; set; }
        [FirestoreProperty]
        public string alliance { get; set; }
    }
    [FirestoreData]
    public class JSONbattleboardAlliances
    {
        [FirestoreProperty]
        public string name { get; set; }
        [FirestoreProperty]
        public int kills { get; set; }
        [FirestoreProperty]
        public int deaths { get; set; }
        [FirestoreProperty]
        public ulong killFame { get; set; }
    }
}