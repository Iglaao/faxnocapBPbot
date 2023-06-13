using faxnocapBPbot.Exceptions;
using faxnocapBPbot.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace faxnocapBPbot.Handlers
{
    public class Battleboard : IBattleboard
    {
        public async Task<(bool, string)> PostBattleboard(string content, string season, string title, params int[] battleId)
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
                    //post bb to firestore
                    await Firestore.UploadFileToFirestore(season, battleboards[0]);
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
