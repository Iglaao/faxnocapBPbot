using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace faxnocapBPbot.JsonModels
{
    public class GuildModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string AllianceTag { get; set; }
        public ulong KillFame { get; set; }
        public ulong DeathFame { get; set; }
        public int MemberCount { get; set; }

    }
}
