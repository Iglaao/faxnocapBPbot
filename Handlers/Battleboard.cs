using faxnocapBPbot.Exceptions;
using faxnocapBPbot.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace faxnocapBPbot.Handlers
{
    public class Battleboard : IBattleboard
    {
        public async Task<(bool, string)> PostBattleboard(string season, string content, string title, params int[] battleId)
        {
            List<string> battleboards = new List<string>();
            try
            {
                foreach (int id in battleId)
                {
                    var bb = await AlbionApiFetcher.FetchAPI(AlbionApiFetcher.BattleUrl + id);
                    battleboards.Add(bb);
                }
                if (battleboards.Count == 1)
                {
                    await Firestore.UploadFileToFirestore(season, title, battleboards[0]);
                }
                else
                {
                    //aggregate all bb into one
                    //post aggregated bb to firestore
                }
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
