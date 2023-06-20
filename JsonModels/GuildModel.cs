using Google.Cloud.Firestore;

namespace faxnocapBPbot.JsonModels
{
    [FirestoreData]
    public class GuildModel
    {
        [FirestoreProperty]
        public string Name { get; set; }
        [FirestoreProperty]
        public string AllianceTag { get; set; }
        [FirestoreProperty]
        public ulong KillFame { get; set; }
        [FirestoreProperty]
        public ulong DeathFame { get; set; }
        [FirestoreProperty]
        public int MemberCount { get; set; }
    }
}
