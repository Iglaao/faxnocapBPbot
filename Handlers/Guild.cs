using faxnocapBPbot.Helpers;
using faxnocapBPbot.Interfaces;
using faxnocapBPbot.JsonModels;
using Google.Cloud.Firestore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections;

namespace faxnocapBPbot.Handlers
{
    public class Guild : IGuild
    {
        private DocumentReference _docRef;
        public async Task<(bool, string)> PostMembersStats(string season)
        {
            try
            {
                Dictionary<string, object> playerData = null;
                var stats = JsonConvert
                    .DeserializeObject<List<PlayerModel>>(await AlbionApiFetcher.FetchAPI(AlbionApiFetcher.GuildUrl + AlbionApiFetcher.GuildId + "/members"));
                foreach(var member in stats)
                {
                    _docRef = Firestore.GetInstance().Collection(season + "members").Document(member.Name);
                    playerData = new Dictionary<string, object>
                    {
                        { DateTime.Now.ToString("dd.MM.yyyy"), member }
                    };
                    await _docRef.SetAsync(playerData, SetOptions.MergeAll);
                }
                await UpdateMemberList(stats, season);
                return (true, "");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool, string)> PostGuildStats(string season)
        {
            try
            {
                _docRef = Firestore.GetInstance().Collection(season).Document("stats");
                var stats = JsonConvert
                    .DeserializeObject<GuildModel>(await AlbionApiFetcher.FetchAPI(AlbionApiFetcher.GuildUrl + AlbionApiFetcher.GuildId));
                Dictionary<string, object> statsWrapper = new Dictionary<string, object>
                {
                    { DateTime.Now.ToString("dd.MM.yyyy"), stats }
                };
                await _docRef.SetAsync(statsWrapper, SetOptions.MergeAll);
                return (true, "");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        private async Task UpdateMemberList(List<PlayerModel> players, string season)
        {
            ArrayList membersArray = new ArrayList();
            players.ForEach(member => membersArray.Add(member.Name));
            _docRef = Firestore.GetInstance().Collection(season + "dict").Document("members");
            Dictionary<string, object> data = new Dictionary<string, object>
            {
                {"members", membersArray},
            };
            await _docRef.SetAsync(data, SetOptions.MergeAll);
        }
    }
}
