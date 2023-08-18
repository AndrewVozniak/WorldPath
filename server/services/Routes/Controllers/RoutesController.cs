using Microsoft.AspNetCore.Mvc;
using Routes.Models;
using Routes.Services;
using Routes.Sync;
using System.Text.Json;

namespace Routes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutesController : ControllerBase
    {
        private readonly IPlacesDataClient _placesDataClient;
        private readonly ITrafficService _trafficService;
        public RoutesController(IPlacesDataClient placesDataClient, ITrafficService trafficService)
        {
            _placesDataClient = placesDataClient;
            _trafficService = trafficService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoutes([FromBody] Coordinate coordinate)
        {
            try
            {
                var place = await _placesDataClient.GetCoordinateFromPlaces(coordinate);
                if (place != null)
                {
                    return RedirectToAction(
                        "GetRoutes",
                        "Routes",
                        new {lat = place.Lat, lon = place.Lon}
                        );
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> --> Could not GET synchronously : {ex.Message}");
            }
            return NotFound();
        }

        [HttpGet]
        [NonAction]
        public async Task<IActionResult> GetRoutes(float lat, float lon)
        {
            var response = await _trafficService.GetTraffic(lat, lon);
            return Ok(response);
        }
    }
}
