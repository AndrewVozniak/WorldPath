using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Places_Service.Data;
using Places_Service.Models;

namespace Places_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlacesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PlacesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetPlaceByCoordinates([FromQuery] float lat,[FromQuery] float lon)
        {
            try
            {
                var apiKey = "AIzaSyCmCZMRuxBtuJmFRJAuGKhobIExrksG4l0";
                var radius = 1000;
                var culture = CultureInfo.InvariantCulture;
                var latFormatted = lat.ToString(culture);
                var lonFormatted = lon.ToString(culture);

                using (HttpClient client = new HttpClient())
                {
                    var url = $"https://maps.googleapis.com/maps/api/place/nearbysearch/json?location={latFormatted},{lonFormatted}&radius={radius}&key={apiKey}";

                    Console.WriteLine(url);
                    HttpResponseMessage response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        var placesData = JsonConvert.DeserializeObject<PlacesApiResponse>(jsonResponse);

                        var newPlaces = new List<Place>();

                        if (placesData != null)
                        {
                            foreach (var result in placesData.Results)
                            {
                                var placeType = string.Join(",", result.Types);
                            
                                if (!_context.Places.Any(p => p.Name == result.Name
                                    && p.Lat == result.Geometry.Location.Lat
                                    && p.Lon == result.Geometry.Location.Lng))
                                {
                                    var place = new Place
                                    {
                                        Name = result.Name,
                                        Lat = result.Geometry.Location.Lat,
                                        Lon = result.Geometry.Location.Lng,
                                        PlaceType = placeType,
                                        CreatedAt = DateTime.Now
                                    };
                                    newPlaces.Add(place);
                                    _context.Places.Add(place);
                                }
                                else
                                {
                                    var place = new Place
                                    {
                                        Name = result.Name,
                                        Lat = result.Geometry.Location.Lat,
                                        Lon = result.Geometry.Location.Lng,
                                        PlaceType = placeType
                                    };
                                    newPlaces.Add(place);
                                }
                            }
                        }
                        else
                        {
                            return NotFound("--> Responce from Google is null");
                        }
                        
                        await _context.SaveChangesAsync();

                        return Ok(newPlaces);
                    }
                    else
                    {
                        return BadRequest("Error fetching data from Google Places API.");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}    
           