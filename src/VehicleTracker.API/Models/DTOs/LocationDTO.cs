using MongoDB.Bson;
using System;

namespace VehicleTracker.API.Models.DTOs
{
    public class LocationDTO
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string VehicleId { get; set; }
        public string DeviceId { get; set; }
        public DateTime UpdateLocationTimeStamp { get; set; } = DateTime.UtcNow;
    }
}
