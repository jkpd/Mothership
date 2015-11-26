using MongoDB.Bson;
using NUnit.Framework;
using RealEstate.Rentals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Rentals
{
    [TestFixture]
    class RentalsTests : AssertionHelper
    {
        [Test]
        public void ToDocument_RentalWithPrice()
        {
            var rental = new Rental();
            rental.Price = 1;

            var document = rental.ToBsonDocument();
            Expect(document["Price"].BsonType, Is.EqualTo(BsonType.Double));
        }

        [Test]
        public void ToDocument_RentalWithId()
        {
            var rental = new Rental();
            rental.Id = ObjectId.GenerateNewId().ToString();
            var document = rental.ToBsonDocument();
            Expect(document["_id"].BsonType, Is.EqualTo(BsonType.ObjectId));
        }
    }
}
