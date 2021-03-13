using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleTracker.API.Models
{
    public class Location : BaseEntity
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime UpdateLocationTimeStamp { get; set; } = DateTime.UtcNow;
    }
}
