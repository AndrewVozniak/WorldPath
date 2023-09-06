using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Routes_Service.Dtos;
using Route = Routes_Service.Dtos.Route;

namespace Routes_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public RouteController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpPost]
        public async Task<IActionResult> GetRouteAsync([FromBody] RouteRequest request)
        {
            var url = $"https://maps.googleapis.com/maps/api/directions/json?origin={request.StartLocation}&destination={request.EndLocation}&key={request.Key}";

            var response = await _httpClient.GetAsync(url);
            
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("The request failed with status code " + response.StatusCode);
            }

            try
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var routeResponse = JsonConvert.DeserializeObject<RouteResponse>(jsonResponse);
                List<Route> routes = routeResponse?.Routes;

                return Ok(routes);
            }
            catch (Exception ex)
            {
                
            }

            return NotFound();
        }
    }
}
