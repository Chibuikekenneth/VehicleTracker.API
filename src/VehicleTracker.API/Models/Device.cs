using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace VehicleTracker.API.Models
{
    public class Device : BaseEntity
    {
        public string Name { get; set; }

        public List<Location> Locations { get; set; }
    }
}
