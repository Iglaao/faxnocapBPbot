using Google.Cloud.Firestore;
using faxnocapBPbot.ConfigStructs;

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
