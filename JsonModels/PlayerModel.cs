using Google.Cloud.Firestore;
using System.Collections.Generic;

namespace faxnocapBPbot.JsonModels
{
    [FirestoreData]
    public class PlayerModel
    {
        [FirestoreProperty]
        public string Name { get; set; }
        [FirestoreProperty]
        public ulong KillFame { get; set; }
        [FirestoreProperty]
        public ulong DeathFame { get; set; }
        [FirestoreProperty]
        public float FameRatio { get; set; }
        [FirestoreProperty]
        public PlayerLifetimeStatisticsModel LifetimeStatistics { get; set; }
    }
    [FirestoreData]
    public class PlayerLifetimeStatisticsModel
    {
        [FirestoreProperty]
        public PveModel PvE { get; set; }
        [FirestoreProperty]
        public ulong FishingFame { get; set; }
        [FirestoreProperty]
        public Dictionary<string, GatheringModel> Gathering { get; set; } 
    }
    [FirestoreData]
    public class PveModel
    {
        [FirestoreProperty]
        public ulong Total { get; set; }
        [FirestoreProperty]
        public ulong Royal { get; set; }
        [FirestoreProperty]
        public ulong Outlands { get; set; }
        [FirestoreProperty]
        public ulong Avalon { get; set; }
        [FirestoreProperty]
        public ulong Hellgate { get; set; }
        [FirestoreProperty]
        public ulong CorruptedDungeon { get; set; }
        [FirestoreProperty]
        public ulong Mists { get; set; }
    }
    [FirestoreData]
    public class GatheringModel
    {
        [FirestoreProperty]
        public ulong Total { get; set; }
    }
    [FirestoreData]
    public class CraftingModel
    {
        [FirestoreProperty]
        public ulong Total { get; set; }
    }
}
