using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace faxnocapBPbot.JsonModels
{
    public class JSONplayer
    {
        public string Name { get; set; }
        public string GuildName { get; set; }
        public string AllianceName { get; set; }
        public ulong DeathFame { get; set; }
        public ulong KillFame { get; set; }
        public float FameRatio { get; set; }
    }
}
