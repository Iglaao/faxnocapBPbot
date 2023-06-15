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

namespace faxnocapBPbot
{
    public static class Firestore
    {
        public static async Task UploadFileToFirestore(string collectionName, string title, string json)
        {
            var firestoreConfig = ConfigDeserializator.ReturnDeserializedJson<FirestoreConfig>("firestoreConfig.json");
            FirestoreDb db = FirestoreDb.Create(firestoreConfig.Project_id);
            JSONbattleboard battleboard = Newtonsoft.Json.JsonConvert.DeserializeObject<JSONbattleboard>(json);
            battleboard.title = title;
            DocumentReference docRef = db.Collection(collectionName).Document(battleboard.startTime.ToString("dd.MM.yyyy"));
            Dictionary<string, object> bbWrapper = new Dictionary<string, object>
            {
                { battleboard.id.ToString(), battleboard },
            };
            await docRef.SetAsync(bbWrapper, SetOptions.MergeAll);
        }
    }
}
