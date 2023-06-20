using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
