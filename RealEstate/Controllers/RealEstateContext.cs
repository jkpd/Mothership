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
            //Connect to CoreControl
            //var client = new MongoClient("mongodb://MongoDB-1.corethree-ltda.8260.mongodbdns.com");
            //Database = client.GetDatabase("Corethree");

            var client = new MongoClient(Settings.Default.RealEstateConnectionString);
            Database = client.GetDatabase(Settings.Default.RealEstateDatabaseName);            
        }

        public IMongoCollection<Rental> Rentals
        {
            get
            {
                return Database.GetCollection<Rental>("rentals");
            }
        }

        public IMongoCollection<Models.GenericData_Alerts> GenericData_Alerts
        {
            get
            {
                return Database.GetCollection<Models.GenericData_Alerts>("GenericData_Alerts"); ;
            }
        }


    }
}