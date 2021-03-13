using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleTracker.API.Models;
using VehicleTracker.API.Models.DTOs;

namespace VehicleTracker.API.Repository
{
    public interface IVehicleRepository
    {
        Task<Vehicle> RegisterVehicle(VehicleDTO vehicle);
        Task<Location> RecordVehiclePosition(LocationDTO vehiclePosition);
        Task<Location> GetCurrentVehiclePosition(string vehicleId, string deviceId);
        Task<List<Location>> GetVehiclePositionRange(LocationRangeDTO locationRangeDTO);
        Task<bool> CheckVehicleExist(string vehicleRegisterNumber);
    }
}
