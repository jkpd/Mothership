﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstate.Rentals
{
    public class Rental
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Description { get; set; }
        public int NumberOfRooms { get; set; }
        public List<string> Address = new List<string>();
        [BsonRepresentation(BsonType.Double)]
        public decimal Price { get; set; }
        public List<PriceAdjustment> Adjustments = new List<PriceAdjustment>();

        public Rental()
        {

        }

        public Rental(PostRental postRental)
        {
            Description = postRental.Description;
            NumberOfRooms = postRental.NumberOfRooms;
            Price = postRental.Price;
            Address = (postRental.Address ?? string.Empty).Split('\n').ToList();
        }

        public void AdjustPrice(AdjustPrice adjustPrice)
        {
            Adjustments.Add(new PriceAdjustment(adjustPrice, Price));
            Price = adjustPrice.NewPrice;
        }
    }
}