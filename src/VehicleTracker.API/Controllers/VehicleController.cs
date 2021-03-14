using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using VehicleTracker.API.Models;
using VehicleTracker.API.Models.DTOs;
using VehicleTracker.API.Services;

namespace VehicleTracker.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleController : ControllerBase
    {
        private readonly ILogger<VehicleController> _logger;
        private readonly IVehicleService _vehicleService;

        public VehicleController(ILogger<VehicleController> logger, IVehicleService vehicleService)
        {
            _logger = logger;
            _vehicleService = vehicleService;
        }

        /// <summary>
        /// Endpoint to Register a Vehicle
        /// </summary>
        [HttpPost]
        [Route("registerVehicle")]
        public async Task<ActionResult<Vehicle>> RegisterVehicle(VehicleDTO vehicle)
        {
            var newVehicle = await _vehicleService.RegisterVehicle(vehicle);
            return Ok(newVehicle);
        }

        /// <summary>
        /// Endpoint to Record/update a vehicle position (every 30 secs)
        /// </summary>
        [HttpPut]
        [Route("recordVehiclePosition")]
        public async Task<ActionResult<Location>> RecordVehiclePosition(LocationDTO location)
        {
            var newLocation = await _vehicleService.RecordVehiclePosition(location);
            return Ok(newLocation);
        }

        //For Authenticated Administrators (Notice that i'm using Authorize attribute)

        /// <summary>
        /// Endpoint to retrieve the current position of a vehicle (with matching locality name)
        /// </summary>
        [HttpGet]
        //[Authorize]
        [Route("getVehiclePosition")]
        public async Task<ActionResult> GetVehicleCurrentPosition(string vehicleId, string deviseId)
        {
            var currentLocation = await _vehicleService.RetrieveCurrentVehiclePosition(vehicleId, deviseId);

            //Get Matching locality using Google Map's API
            var matchingLocalityName = await _vehicleService.GoogleMatchingLocality(currentLocation.Latitude + "," + currentLocation.Longitude);
            return Ok(new
            {
                Longitude = currentLocation.Longitude,
                Latitude = currentLocation.Latitude,
                MatchingLocalityName = matchingLocalityName,
                UpdateLocationTimeStamp = currentLocation.UpdateLocationTimeStamp,
            });
        }

        //For Authenticated Administrators (Notice that i'm using Authorize attribute)

        /// <summary>
        /// Endpoint to retrieve the positions of a vehicle during a certain time
        /// </summary>
        [HttpGet]
        //[Authorize]
        [Route("getVehiclePositionRange")]
        public async Task<ActionResult<Location>> GetVehicleCurrentPositionRange([FromQuery] LocationRangeDTO locationRangeDTO)
        {
            var currentLocationRange = await _vehicleService.RetrieveVehiclePositionWithRange(locationRangeDTO);
            if (currentLocationRange == null) return NotFound();
            return Ok(currentLocationRange);
        }
    }
}
