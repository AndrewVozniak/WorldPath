using Microsoft.AspNetCore.Mvc;
using Places_Service.Models;
using Places_Service.Repository.Interfaces;
using Places_Service.Services;

namespace Places_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlacesController : ControllerBase
    {
        private readonly IPlaceRepository _repository;
        private readonly IGooglePlaceService _placeService;

        public PlacesController(IPlaceRepository repository, IGooglePlaceService placeService)
        {
            _repository = repository;
            _placeService = placeService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetPlacesById([FromQuery] int id)
        {
            var place = await _repository.GetPlacesById(id);

            if (place == null) return NotFound();

            return Ok(place);
        }

        [HttpGet]
        public async Task<IActionResult> GetPlacesByName([FromQuery] string name)
        {
            var place = await _repository.GetPlaceByName(name);

            if (place == null) return NotFound();

            return Ok(place);
        }

        [HttpGet]
        public async Task<IActionResult> GetPlacesByCoordinate([FromQuery] float lat, [FromQuery] float lon)
        {
            var place = await _repository.GetPlaceByCoordinate(lat, lon);

            if (place != null) return Ok(place);
            
            var newPlaces = new List<Place>();
            
            var placeData = await _placeService.GetPlaceByCoordinate(lat, lon);

            if (placeData != null)
            {
                foreach (var result in placeData.Results)
                {   
                    var placeType = string.Join(",", result.Types);
                    
                    var newPlace = new Place
                    {
                        Name = result.Name,
                        Lat = result.Geometry.Location.Lat,
                        Lon = result.Geometry.Location.Lng,
                        PlaceType = placeType,
                        CreatedAt = DateTime.Now
                    };
                    newPlaces.Add(newPlace);
                    await _repository.SaveAsync();
                }
                return Ok(newPlaces);
            }
            else
            {
                return BadRequest("Error fetching data from Google Places API.");
            }
        }
    }
}    
            