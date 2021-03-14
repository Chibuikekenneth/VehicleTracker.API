using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _configuration;
        public VehicleService(IVehicleRepository vehicleRepository, IConfiguration configuration)
        {
            _vehicleRepository = vehicleRepository;
            _configuration = configuration;
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

        public async Task<List<Location>> RetrieveVehiclePositionWithRange(LocationRangeDTO locationRangeDTO)
        {
            if (locationRangeDTO == null) throw new KeyNotFoundException("location Range paramters are empty");
            return await _vehicleRepository.GetVehiclePositionRange(locationRangeDTO);
        }

        //using Reverse geocoding (address lookup) in Google's Geocoding API
        public async Task<string> GoogleMatchingLocality(string location)
        {
            // Provide api key code
            string YOUR_API_KEY = _configuration["GoogleMap:ApiKey"];
            string requestUri = string.Format("https://maps.googleapis.com/maps/api/geocode/json?latlng={0}&key={1}", Uri.EscapeDataString(location), YOUR_API_KEY);

            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            Rootobject deserialized = JsonConvert.DeserializeObject<Rootobject>(result);

            if (deserialized.results.Count() < 1) throw new InvalidOperationException("Matching Name locality not found, Check your API KEY or any other related data");

            return deserialized.results.Select(x => new AddressComponent
            {
                long_name = x.address_components.FirstOrDefault().long_name
            }).Where(x => x.types.ToString() == "sublocality").ToString();
        }

    }

}
