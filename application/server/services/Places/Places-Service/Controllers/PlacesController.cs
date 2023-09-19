using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Places_Service.Dtos;
using Places_Service.Models;
using Places_Service.Services;
using ZstdSharp.Unsafe;

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
            var parsedPhotos = new List<ParsedPlacePhoto>();
            
            var placeData = await _googlePlaceService.GetPlaceByCoordinate(lat, lon);
            
            if (placeData != null)
            {
                for (var i = 0; i < placeData.Results.Count; i++)
                {
                    var result = placeData.Results[i];
    
                    var placeType = string.Join(",", result.Types);
                    
                    var newPlace = new Place
                    { 
                        Id = ObjectId.GenerateNewId().ToString(),
                        Name = result.Name,
                        Lat = result.Geometry.Location.Lat,
                        Lon = result.Geometry.Location.Lng,
                        PlaceType = placeType,
                        CreatedAt = DateTime.Now,
                        PhotoReference = "nothing there"
                    };
                    
                    if (result.Photos != null && i < result.Photos.Count)
                    {
                        newPlace.PhotoReference = result.Photos[i].PhotoReference; // Встановлюємо PhotoReference, якщо фотографія доступна
                    }

                    newPlaces.Add(newPlace);

                    if (result.Photos != null && result.Photos.Count > 0)
                    {
                        for (var j = 0; j < result.Photos.Count; j++)
                        {
                            var photo = result.Photos[j];

                            var parsedPhoto = new ParsedPlacePhoto()
                            {
                                PlaceId = newPlace.Id,
                                PhotoPath = newPlace.PhotoReference, // Використовуємо PhotoReference з newPlace
                                CreatedAt = DateTime.Now
                            };
                            parsedPhotos.Add(parsedPhoto);
                        }
                    }
                }


                await _placeService.AddManyPlacesAsync(newPlaces);
                if (parsedPhotos.Count > 0 && parsedPhotos != null)
                    await _placeService.AddManyParsedPlacePhotos(parsedPhotos);
    
                return Ok(newPlaces);
            }
            else
            {
                return BadRequest("Error fetching data from Google Places API.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Like([FromBody] PlaceLikeDto placeLikeDto)
        {
            var placeExists = await _placeService.PlaceExistsAsync(placeLikeDto.PlaceId);
        
            if (!placeExists)
            {
                return NotFound("The specified PlaceId does not exist.");
            }

            var placeLikeFromDb = await _placeService.FindPlaceLikeAsync(placeLikeDto.PlaceId, placeLikeDto.UserId);
        
            if (placeLikeFromDb is not null)
            {
                await _placeService.DeletePlaceLikeAsync(placeLikeDto.PlaceId, placeLikeDto.UserId);
                return Ok("Dislike was Ok!");
            }

            var placeLike = _mapper.Map<PlaceLike>(placeLikeDto);

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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlace(string id, [FromBody] PlaceDto updatePlaceDto)
        {
            var success = await _placeService.UpdatePlaceAsync(id, _mapper.Map<Place>(updatePlaceDto));
            
            if (!success) return NotFound("Place not found");
            
            return Ok("Place updated successfully");
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePlace(string placeId)
        {
            var isExists = await _placeService.PlaceExistsAsync(placeId);

            if (!isExists) return NotFound();

            await _placeService.DeletePlaceAsync(placeId);

            return Ok("Place deleted successfully");
        }
    }
}    
            