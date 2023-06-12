using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace faxnocapBPbot.Interfaces
{
    public interface IBattleboard
    {
        Task PostBattleboard();
        Task UpdateBattleboard();
        Task RemoveBattleboard();
    }
}
