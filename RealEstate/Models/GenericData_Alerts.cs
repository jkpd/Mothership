using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstate.Models
{
    [BsonIgnoreExtraElements]
    public class GenericData_Alerts
    {
        public string Id { get; set; }
        public string Title { get; set; }

        public GenericData_Alerts()
        {

        }
    }
}