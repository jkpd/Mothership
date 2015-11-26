using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class BsonDocumentTests
    {
        public BsonDocumentTests()
        {
        }

        [Test]
        public void EmptyDocument()
        {
            var document = new BsonDocument();
            Console.WriteLine(document);
        }

        [Test]
        public void AddElements()
        {
            var person = new BsonDocument
            {
                {"Age", 54 },
                {"IsTall", true }
            };
            person.Add("FirstName", "Yannnis");
            person.Add("Rank", new BsonInt32(10));
            Console.WriteLine(person);
        }

        [Test]
        public void AddArrays()
        {
            var person = new BsonDocument
            {
                {"MembersOfFamily", new BsonArray(new [] { "Pops", "Thalis"})}
            };
            Console.WriteLine(person);
        }

        [Test]
        public void AddSubDocument()
        {
            var person = new BsonDocument
            {
                {"MembersOfFamily", new BsonDocument { { "isSubDocument", true} } }
            };
            Console.WriteLine(person);
        }

        [BsonIgnoreExtraElements]
        public class Person
        {
            //id, ID, _id
            [BsonId]
            public string MyStrageId { get; set; }
            public int Age { get; set; }
            public string FirstName { get; set; }
            public List<string> Address { get; set; }
            public Contact Contact = new Contact();
            [BsonIgnore]
            public string IgnoreMe { get; set; }
            [BsonIgnoreIfNull]
            public string IgnoreMeIfNull { get; set; }
            [BsonElement("New")]
            public string Old { get; set; }
            public double NetValue { get; set; }
            [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
            public DateTime Date { get; set; }
            [BsonDateTimeOptions(DateOnly = true)]
            public DateTime DateOnly { get; set; }

            //[BsonElement]
            private string _private;
        }

        public class Contact
        {
            public string Name { get; set; }
            public string PhoneNumber { get; set; }
        }

        [Test]
        public void AddPoco()
        {
            var person = new Person
            {
                Age = 46,
                FirstName = "Yannis",
                NetValue = 100.5,
                Date = DateTime.Now,
            };
            person.Address = new List<string>();
            person.Address.AddRange(new[] { "Irakleous 7", "Proodou 5" });
            person.Contact.Name = "Manolis";
            person.Contact.PhoneNumber = "123456789";

            Console.WriteLine(person.ToJson());
        }
    }
}
