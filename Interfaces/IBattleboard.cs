using System.Threading.Tasks;

namespace faxnocapBPbot.Interfaces
{
    public interface IBattleboard
    {
        Task<(bool, string)> PostBattleboard(string season, string title, params int[] battleId);
        Task UpdateBattleboard(string season, params int[] id);
        Task RemoveBattleboard(string season, string id);
    }
}