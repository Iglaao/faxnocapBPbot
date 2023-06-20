using System.Threading.Tasks;

namespace faxnocapBPbot.Interfaces
{
    public interface IBattleboard
    {
        Task<(bool, string)> PostBattleboard(string season, string title, params int[] battleId);
        Task<(bool, string)> RemoveBattleboard(string season, string date, string id);
    }
}