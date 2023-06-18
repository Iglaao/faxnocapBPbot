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
    public sealed class Firestore
    {
        private static FirestoreDb _instance;
        private Firestore() { }
        public static FirestoreDb GetInstance()
        {
            if(_instance == null)
            {
                _instance = FirestoreDb.Create(
                    ConfigDeserializator
                    .ReturnDeserializedJson<FirestoreConfig>("firestoreConfig.json")
                    .Project_id
                    );
            }
            return _instance;
        }
    }
}
