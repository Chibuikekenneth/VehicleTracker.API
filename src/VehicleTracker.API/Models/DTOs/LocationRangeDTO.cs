using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleTracker.API.Models.DTOs
{
    public class LocationRangeDTO
    {
       public string VehicleId { get; set; }
       public string DeviceId { get; set; }
       public DateTime StartDate { get; set; }
       public DateTime EndDate { get; set; }
    }
}
