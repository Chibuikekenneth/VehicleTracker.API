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

        [HttpPost]
        [Route("registerVehicle")]
        public async Task<ActionResult<Vehicle>> RegisterVehicle(VehicleDTO vehicle)
        {
            var newVehicle = await _vehicleService.RegisterVehicle(vehicle);
            return Ok(newVehicle);
        }

        [HttpPost]
        [Route("recordVehiclePosition")]
        public async Task<ActionResult<Location>> RecordVehiclePosition(LocationDTO location)
        {
            var newLocation = await _vehicleService.RecordVehiclePosition(location);
            return Ok(newLocation);
        }

        [HttpGet]
        [Route("getVehiclePosition")]
        public async Task<ActionResult<Location>> GetVehicleCurrentPosition(string vehicleId, string deviseId)
        {
            var currentLocation = await _vehicleService.RetrieveCurrentVehiclePosition(vehicleId, deviseId);
            return Ok(currentLocation);
        }
    }
}
