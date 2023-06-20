using System.Threading.Tasks;

namespace faxnocapBPbot.Interfaces
{
    public interface IGuild
    {
        Task<(bool, string)> PostGuildStats(string season);
        Task<(bool, string)> PostMembersStats(string season);
    }
}
