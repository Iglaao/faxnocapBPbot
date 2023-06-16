using Google.Cloud.Firestore;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System;
using faxnocapBPbot.ConfigStructs;
using faxnocapBPbot.JsonModels;
using Newtonsoft.Json;

namespace faxnocapBPbot
{
    public static class Firestore
    {
        public static async Task UploadFileToFirestore(string collectionName, string title, string json)
        {
            //Setting connection to db
            var firestoreConfig = ConfigDeserializator.ReturnDeserializedJson<FirestoreConfig>("firestoreConfig.json");
            FirestoreDb db = FirestoreDb.Create(firestoreConfig.Project_id);
            //Deserialization of fetched json
            JSONbattleboard battleboard = JsonConvert.DeserializeObject<JSONbattleboard>(json);
            battleboard.title = title;
            //Uploading Json to db
            DocumentReference docRef = db.Collection(collectionName).Document(battleboard.startTime.ToString("dd.MM.yyyy"));
            Dictionary<string, object> bbWrapper = new Dictionary<string, object>
            {
                { battleboard.id, battleboard },
            };
            await docRef.SetAsync(bbWrapper, SetOptions.MergeAll);
            //Additional collection to store id+title
            docRef = db.Collection(collectionName + "dict").Document("battleboards");
            Dictionary<string, string> battleboardDictionary = new Dictionary<string, string>
            {
                { battleboard.id, battleboard.title }
            };
            await docRef.SetAsync(battleboardDictionary, SetOptions.MergeAll);
        }
        public static async Task UploadCombinedToFirestore(string collectionName, string title, List<string> battleboards)
        {
            var firestoreConfig = ConfigDeserializator.ReturnDeserializedJson<FirestoreConfig>("firestoreConfig.json");
            FirestoreDb db = FirestoreDb.Create(firestoreConfig.Project_id);

            List<JSONbattleboard> xbattleboards = new List<JSONbattleboard>();
            battleboards.ForEach(bb => xbattleboards.Add(JsonConvert.DeserializeObject<JSONbattleboard>(bb)));
            JSONbattleboard combinedBattleboard = new JSONbattleboard();
            combinedBattleboard.title = title;
            combinedBattleboard.startTime = xbattleboards[0].startTime;
            
            foreach (var bb in xbattleboards)
            {
                combinedBattleboard.totalKills += bb.totalKills;
                if (String.IsNullOrEmpty(combinedBattleboard.id))
                {
                    combinedBattleboard.id += bb.id;
                }
                else
                {
                    combinedBattleboard.id += ("," + bb.id);
                }

                //players
                foreach (string key in bb.players.Keys)
                {
                    if (combinedBattleboard.players != null && combinedBattleboard.players.ContainsKey(key))
                    {
                        combinedBattleboard.players[key].killfame += bb.players[key].killfame;
                        combinedBattleboard.players[key].kills += bb.players[key].kills;
                        combinedBattleboard.players[key].deaths += bb.players[key].deaths;
                    }
                    else
                    {
                        combinedBattleboard.players.Add(key, bb.players[key]);
                    }
                }
                //guilds
                foreach (string key in bb.guilds.Keys)
                {
                    if (combinedBattleboard.guilds != null && combinedBattleboard.guilds.ContainsKey(key))
                    {
                        combinedBattleboard.guilds[key].killFame += bb.guilds[key].killFame;
                        combinedBattleboard.guilds[key].kills += bb.guilds[key].kills;
                        combinedBattleboard.guilds[key].deaths += bb.guilds[key].deaths;
                    }
                    else
                    {
                        combinedBattleboard.guilds.Add(key, bb.guilds[key]);
                    }
                }
                //alliances
                foreach (string key in bb.alliances.Keys)
                {
                    if (combinedBattleboard.alliances != null && combinedBattleboard.alliances.ContainsKey(key)){
                        combinedBattleboard.alliances[key].killFame += bb.alliances[key].killFame;
                        combinedBattleboard.alliances[key].kills += bb.alliances[key].kills;
                        combinedBattleboard.alliances[key].deaths += bb.alliances[key].deaths;
                    }
                    else
                    {
                        combinedBattleboard.alliances.Add(key, bb.alliances[key]);
                    }
                }
            }
            DocumentReference docRef = db.Collection(collectionName).Document(combinedBattleboard.startTime.ToString("dd.MM.yyyy"));
            Dictionary<string, object> bbWrapper = new Dictionary<string, object>
            {
                { combinedBattleboard.id, combinedBattleboard },
            };
            await docRef.SetAsync(bbWrapper, SetOptions.MergeAll);
        }
    }
}
