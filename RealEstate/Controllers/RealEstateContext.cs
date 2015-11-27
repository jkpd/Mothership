using MongoDB.Bson;
using MongoDB.Driver;
using RealEstate.Properties;
using RealEstate.Rentals;

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

        public IMongoCollection<Rental> Rentals
        {
            get
            {
                return Database.GetCollection<Rental>("rentals");
            }
        }

    }
}