using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Places_Service.Models;
using Places_Service.Services;
using Places.Application.Interfaces;
using Places.Application.Places.Commands.CreateManyPlaces;
using Places.Domain;
using Refit;

namespace Places_Service.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PlacesController : BaseController
    {
        private readonly IGooglePlaceApi _googlePlaceApi;
        private readonly IMongoDb _mongoDb;
        private readonly IMapper _mapper;

        public PlacesController(IMongoDb mongoDb, IMapper mapper, IMediator mediator, IGooglePlaceApi googlePlaceApi): base(mediator)
        {
            _mongoDb = mongoDb;
            _mapper = mapper;
            _googlePlaceApi = googlePlaceApi;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetPlacesById([FromQuery] string id)
        {
            var place = await _mongoDb.GetPlaceById(id);
            
            if (place == null) return NotFound();

            return Ok(place);
        }

        [HttpGet]
        public async Task<IActionResult> GetPlacesByName([FromQuery] string name)
        {
            var place = await _mongoDb.GetPlaceById(name);
            
            if (place == null) return NotFound();
            
            return Ok(place);
        }

        [HttpGet]
        public async Task<IActionResult> GetPlacesByCoordinate([FromQuery] float lat, [FromQuery] float lon)
        {
            var placesNearby = await _mongoDb.FindPlacesNearbyAsync(lat, lon, 1000);
            
            // If places founded
            if (placesNearby != null && placesNearby.Any()) return Ok(placesNearby);
            
            var placesApi = RestService.For<IGooglePlaceApi>("https://maps.googleapis.com/maps/api/place/");
            var placeData = await placesApi.GetPlacesByCoordinate(lat, lon);
            
            var query = new CreatePlacesCommand()
            {
                PlacesApiResponse = placeData
            };

            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Like([FromBody] PlaceLikeDto placeLikeDto)
        {
            var placeExists = await _mongoDb.PlaceExistsAsync(placeLikeDto.PlaceId);
        
            if (!placeExists)
            {
                return NotFound("The specified PlaceId does not exist.");
            }

            var placeLikeFromDb = await _mongoDb.FindPlaceLikeAsync(placeLikeDto.PlaceId, placeLikeDto.UserId);
        
            if (placeLikeFromDb is not null)
            {
                await _mongoDb.DeletePlaceLikeAsync(placeLikeDto.PlaceId, placeLikeDto.UserId);
                return Ok("Dislike was Ok!");
            }

            var placeLike = _mapper.Map<PlaceLike>(placeLikeDto);

            await _mongoDb.AddPlaceLikeAsync(placeLike);
            return Ok(placeLike);
        }

        [HttpPost]
        public async Task<IActionResult> Comment([FromBody] PlaceCommentDto commentDto)
        {
            var placeExists = await _mongoDb.PlaceExistsAsync(commentDto.PlaceId);
        
            if (!placeExists) return NotFound("The specified PlaceId does not exist.");
            
            var placeComment = new PlaceComment
            {
                PlaceId = commentDto.PlaceId,
                UserId = commentDto.UserId,
                Text = commentDto.Text,
            };

            await _mongoDb.AddPlaceCommentAsync(placeComment);
        
            return Ok(placeComment);
        }

        [HttpPost]
        public async Task<IActionResult> AddPlace([FromBody] PlaceDto placeDto)
        {
            var placeFromDb = await _mongoDb.GetPlaceByName(placeDto.Name);

            if (placeFromDb != null) return NotFound("Place already exists");

            var place = _mapper.Map<Place>(placeDto);

            await _mongoDb.AddOnePlaceAsync(place);

            return Ok(place);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlace(string id, [FromBody] PlaceDto updatePlaceDto)
        {
            var success = await _mongoDb.UpdatePlaceAsync(id, _mapper.Map<Place>(updatePlaceDto));
            
            if (!success) return NotFound("Place not found");
            
            return Ok("Place updated successfully");
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePlace(string placeId)
        {
            var isExists = await _mongoDb.PlaceExistsAsync(placeId);

            if (!isExists) return NotFound();

            await _mongoDb.DeletePlaceAsync(placeId);

            return Ok("Place deleted successfully");
        }
    }
}    
            