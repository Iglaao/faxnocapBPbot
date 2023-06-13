using Google.Cloud.Firestore;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System;
using faxnocapBPbot.ConfigStructs;

namespace faxnocapBPbot
{
    public static class Firestore
    {
        public static async Task UploadFileToFirestore(string collectionName, string json)
        {
            var firestoreConfig = ConfigDeserializator.ReturnDeserializedJson<FirestoreConfig>("firestoreConfig.json");
            try
            {
                Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "firestoreConfig.json");
                FirestoreDb db = FirestoreDb.Create(firestoreConfig.Project_id);
                DateTime dateTime = DateTime.Now.Date;
                DocumentReference docRef = db.Collection(collectionName).Document(dateTime.ToString());
                await docRef.SetAsync(JsonDocument.Parse(string.Format("[{0}]", json)));
            }
            catch(Exception ex)
            {
                var test = ex;
            }
            
        }
    }
}
