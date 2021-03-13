using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleTracker.API.Models;
using VehicleTracker.API.Models.DTOs;

namespace VehicleTracker.API.Repository
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly IMongoCollection<Vehicle> _vehicles;
        public VehicleRepository(IVTrackerDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _vehicles = database.GetCollection<Vehicle>(settings.VehiclesCollectionName);
        }

        public async Task<bool> CheckVehicleExist(string registerNumber)
        {
            var vehicleExist = await _vehicles.Find<Vehicle>(v => v.RegistrationNumber == registerNumber).FirstOrDefaultAsync();
            if (vehicleExist != null) return true;
            return false;
        }

        public async Task<Location> GetCurrentVehiclePosition(string vehicleId, string deviceId)
        {
            var filter = Builders<Vehicle>.Filter.And(
            Builders<Vehicle>.Filter.Where(x => x.Id == vehicleId),
            Builders<Vehicle>.Filter.ElemMatch(x => x.Devices, d => d.Id == deviceId));

            var vehicle = await _vehicles.Find(filter).FirstOrDefaultAsync();
            var location = vehicle.Devices.Select(c => c.Locations).FirstOrDefault().OrderByDescending(d => d.UpdateLocationTimeStamp);
            return location.Take(1).FirstOrDefault();
        }

        public Task<List<Location>> GetVehiclePositionRange(LocationRangeDTO locationRangeDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<Location> RecordVehiclePosition(LocationDTO vehiclePosition)
        {
            var filter = Builders<Vehicle>.Filter.And(
            Builders<Vehicle>.Filter.Where(x => x.Id == vehiclePosition.VehicleId),
            Builders<Vehicle>.Filter.ElemMatch(x => x.Devices, d => d.Id == vehiclePosition.DeviceId));

            var newLocation = new Location
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Longitude = vehiclePosition.Longitude,
                Latitude = vehiclePosition.Latitude,
                UpdateLocationTimeStamp = DateTime.UtcNow
            };

            var update = Builders<Vehicle>.Update.Push("Devices.$.Locations", newLocation);
            var vehicle = await _vehicles.FindOneAndUpdateAsync(filter, update);
            if (vehicle == null) throw new InvalidOperationException("Please check the vehicle and device Id's");
            return newLocation;
        }

        public async Task<Vehicle> RegisterVehicle(VehicleDTO vehicle)
        {
            var newVehicle = new Vehicle
            {
                RegistrationNumber = vehicle.RegistrationNumber,
                Make = vehicle.Make,
                Model = vehicle.Model,
                Devices = new List<Device>
                {
                    new Device
                    {
                        //generate new object id for nested class
                        Id = ObjectId.GenerateNewId().ToString(),
                        Name = vehicle.Device.Name,
                        Locations = new List<Location>()
                    }
                }
            };
            await _vehicles.InsertOneAsync(newVehicle);
            return newVehicle;
        }
    }
}
