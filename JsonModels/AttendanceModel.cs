using System.Collections.Generic;

namespace faxnocapBPbot.JsonModels
{
    public class AttendanceModel
    {
        public Dictionary<string, AttendanceDateModel> Date { get; set; }
    }
    public class AttendanceDateModel
    {
        public Dictionary<string, PlayerAttendanceStatsModel> PlayersAttendance { get; set; }
    }
    public class PlayerAttendanceStatsModel
    {
        public int Attendance { get; set; }
        public int Kills { get; set; }
        public int Deaths { get; set; }
    }
}
