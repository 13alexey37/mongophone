using System.Configuration;
using MongoDB.Driver;

namespace bdphones.model
{
    public class DBManager
    {
        private static DBManager instance;
        private MongoClient client;
        private IMongoDatabase db;
        private IMongoCollection<Phone> coll;

        public static DBManager Instance => (instance ?? new DBManager());
        public IMongoCollection<Phone> Coll => coll;

        private DBManager()
        {
            client = new MongoClient(ConfigurationManager.ConnectionStrings["MongoDb"].ConnectionString );
            db = client.GetDatabase("phones");
            coll = db.GetCollection<Phone>("phones");
        }
    }
}