using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleTracker.API.Models.GoogleMapModel
{
    public class Rootobject
    {
        public Result[] results { get; set; }
        public string status { get; set; }
    }
}
