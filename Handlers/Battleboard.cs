using faxnocapBPbot.Exceptions;
using faxnocapBPbot.Helpers;
using faxnocapBPbot.Interfaces;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace faxnocapBPbot.Handlers
{
    public class Battleboard : IBattleboard
    {
        public async Task<(bool, string)> PostBattleboard(string season, string title, params int[] battleId)
        {
            List<string> battleboards = new List<string>();
            try
            {
                foreach (int id in battleId) battleboards.Add(await AlbionApiFetcher.FetchAPI(AlbionApiFetcher.BattleUrl + id));
                
                var battleboard = BattleboardAggregator.CombineBattleboards(title, battleboards);

                DocumentReference docRef = Firestore.GetInstance().Collection(season).Document(battleboard.StartTime.ToString("dd.MM.yyyy"));
                Dictionary<string, object> bbWrapper = new Dictionary<string, object>
                {
                    { battleboard.Id, battleboard },
                };
                await docRef.SetAsync(bbWrapper, SetOptions.MergeAll);
                return (true, "");
            }
            catch (ApiException ex)
            {
                return (false, ex.Message);
            }
        }
        public Task RemoveBattleboard(string season, string id)
        {
            throw new NotImplementedException();
        }
        public Task UpdateBattleboard(string season, params int[] id)
        {
            throw new NotImplementedException();
        }
    }
}
