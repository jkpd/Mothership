using MongoDB.Bson;
using MongoDB.Driver;
using RealEstate.Properties;

namespace RealEstate.Controllers
{
    public class RealEstateContext
    {
        public IMongoDatabase Database;

        public RealEstateContext()
        {
            var client = new MongoClient(Settings.Default.RealEstateConnectionString);
            Database = client.GetDatabase(Settings.Default.RealEstateDatabaseName);
            //var collection = Database.GetCollection<BsonDocument>("test");
        }

    }
}