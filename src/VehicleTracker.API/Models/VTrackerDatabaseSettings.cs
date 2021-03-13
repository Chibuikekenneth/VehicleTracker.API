using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleTracker.API.Models
{
    public class VTrackerDatabaseSettings : IVTrackerDatabaseSettings
    {
        public string VehiclesCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
    public interface IVTrackerDatabaseSettings
    {
        string VehiclesCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
