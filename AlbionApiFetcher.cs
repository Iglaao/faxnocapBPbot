using faxnocapBPbot.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace faxnocapBPbot
{
    public static class AlbionApiFetcher
    {
        public static string GuildUrl = "https://gameinfo.albiononline.com/api/gameinfo/guilds/";
        public static string BattleUrl = "https://gameinfo.albiononline.com/api/gameinfo/battles/";

        public static async Task<string> FetchAPI(string apiUrl)    //throws ApiException
        {
            using(HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return result;
                    }
                    else
                    {
                        throw new ApiException($"API request failed with status code: {response.StatusCode}");
                    }
                }
                catch(Exception ex)
                {
                    throw new ApiException($"An error occurred: {ex.Message}", ex);
                }
            }
        }
    }
}
