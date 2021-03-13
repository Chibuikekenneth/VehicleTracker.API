using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using VehicleTracker.API.Models;
using VehicleTracker.API.Models.DTOs;
using VehicleTracker.API.Models.GoogleMapModel;
using VehicleTracker.API.Repository;

namespace VehicleTracker.API.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;
        public VehicleService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<Vehicle> RegisterVehicle(VehicleDTO vehicle)
        {
            var vehicleExist = await _vehicleRepository.CheckVehicleExist(vehicle.RegistrationNumber);
            if (vehicleExist == true) throw new InvalidOperationException($"vehicle with registration number &{vehicle.RegistrationNumber} already exist");
            return await _vehicleRepository.RegisterVehicle(vehicle);
        }

        public async Task<Location> RecordVehiclePosition(LocationDTO vehiclePosition)
        {
            if (vehiclePosition == null) throw new InvalidOperationException($"location cannot be null");
            return await _vehicleRepository.RecordVehiclePosition(vehiclePosition);
        }

        public async Task<Location> RetrieveCurrentVehiclePosition(string vehicleid, string deviceid)
        {
            if (String.IsNullOrEmpty(deviceid)) throw new KeyNotFoundException("deviceid is empty");
            return await _vehicleRepository.GetCurrentVehiclePosition(vehicleid, deviceid);
        }

        public Task<List<Location>> RetrieveVehiclePositionWithRange(LocationRangeDTO locationRangeDTO)
        {
            throw new NotImplementedException();
        }

        //using Reverse geocoding (address lookup) in Google's Geocoding API
        public async Task<string> GetMatchingLocality(string position)
        {
            // Provide api key code
            string YOUR_API_KEY = "";
            string requestUri = string.Format("https://maps.googleapis.com/maps/api/place/nearbysearch/json?latlng={0}&key={1}", Uri.EscapeDataString(position), YOUR_API_KEY);

            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            Rootobject deserialized = JsonConvert.DeserializeObject<Rootobject>(result);

            return deserialized.results.Select(x => new AddressComponent
            {
                long_name = x.address_components.FirstOrDefault().long_name
            }).Where(x => x.types.ToString() == "sublocality").ToString();
        }

    }

}
