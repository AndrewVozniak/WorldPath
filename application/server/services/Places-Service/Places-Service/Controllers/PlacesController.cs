using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Places_Service.Dtos;
using Places_Service.Models;
using Places_Service.Services;

namespace Places_Service.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PlacesController : ControllerBase
    {
        private readonly IGooglePlaceService _googlePlaceService;
        private readonly PlaceService _placeService;
        private readonly IMapper _mapper;

        public PlacesController(IGooglePlaceService googlePlaceService, PlaceService placeService, IMapper mapper)
        {
            _googlePlaceService = googlePlaceService;
            _placeService = placeService;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetPlacesById([FromQuery] string id)
        {
            var place = await _placeService.GetPlaceById(id);
            
            if (place == null) return NotFound();

            return Ok(place);
        }

        [HttpGet]
        public async Task<IActionResult> GetPlacesByName([FromQuery] string name)
        {
            var place = await _placeService.GetPlaceById(name);
            
            if (place == null) return NotFound();
            
            return Ok(place);
        }

        [HttpGet]
        public async Task<IActionResult> GetPlacesByCoordinate([FromQuery] float lat, [FromQuery] float lon)
        {
            var placesNearby = await _placeService.FindPlacesNearbyAsync(lat, lon);
            
            if (placesNearby != null && placesNearby.Any()) return Ok(placesNearby);
            
            var newPlaces = new List<Place>();
            
            var placeData = await _googlePlaceService.GetPlaceByCoordinate(lat, lon);
            
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
                }

                await _placeService.AddManyPlacesAsync(newPlaces);
                return Ok(newPlaces);
            }
            else
            {
                return BadRequest("Error fetching data from Google Places API.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Like([FromBody] PlaceLikeDto like)
        {
            var placeExists = await _placeService.PlaceExistsAsync(like.PlaceId);
        
            if (!placeExists)
            {
                return NotFound("The specified PlaceId does not exist.");
            }

            var placeLikeFromDb = await _placeService.FindPlaceLikeAsync(like.PlaceId, like.UserId);
        
            if (placeLikeFromDb is not null)
            {
                await _placeService.DeletePlaceLikeAsync(like.PlaceId, like.UserId);
                return Ok("Dislike was Ok!");
            }
        
            var placeLike = new PlaceLike()
            {
                PlaceId = like.PlaceId,
                UserId = like.UserId,
            };

            await _placeService.AddPlaceLikeAsync(placeLike);
            return Ok(placeLike);
        }

        [HttpPost]
        public async Task<IActionResult> Comment([FromBody] PlaceCommentDto commentDto)
        {
            var placeExists = await _placeService.PlaceExistsAsync(commentDto.PlaceId);
        
            if (!placeExists) return NotFound("The specified PlaceId does not exist.");
            
            var placeComment = new PlaceComment
            {
                PlaceId = commentDto.PlaceId,
                UserId = commentDto.UserId,
                Text = commentDto.Text,
            };

            await _placeService.AddPlaceCommentAsync(placeComment);
        
            return Ok(placeComment);
        }

        [HttpPost]
        public async Task<IActionResult> AddPlace([FromBody] PlaceDto placeDto)
        {
            var placeFromDb = await _placeService.GetPlaceByName(placeDto.Name);

            if (placeFromDb != null) return NotFound("Place already exists");

            var place = _mapper.Map<Place>(placeDto);

            await _placeService.AddOnePlaceAsync(place);

            return Ok(place);
        }
    }
}    
            