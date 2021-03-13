using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleTracker.API.Models;
using VehicleTracker.API.Models.DTOs;

namespace VehicleTracker.API.Services
{
    public interface IVehicleService
    {
        Task<Vehicle> RegisterVehicle(VehicleDTO vehicle);
        Task<Location> RecordVehiclePosition(LocationDTO vehiclePosition);
        Task<Location> RetrieveCurrentVehiclePosition(string vehicleId, string deviceId);
        Task<List<Location>> RetrieveVehiclePositionWithRange(LocationRangeDTO locationRangeDTO);
        Task<string> GetMatchingLocality(string position);
    }
}
