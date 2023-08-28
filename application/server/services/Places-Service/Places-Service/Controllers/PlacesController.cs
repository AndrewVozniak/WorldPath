using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Places_Service.Data;
using Places_Service.Dtos;
using Places_Service.Models;
using Places_Service.Repository.Interfaces;
using Places_Service.Services;

namespace Places_Service.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PlacesController : ControllerBase
    {
        private readonly IPlaceRepository _repository;
        private readonly IGooglePlaceService _placeService;
        private readonly AppDbContext _context;

        public PlacesController(IPlaceRepository repository, IGooglePlaceService placeService, AppDbContext context)
        {
            _repository = repository;
            _placeService = placeService;
            _context = context;
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
                    await _context.Places.AddAsync(newPlace);
                }
                
                await _context.SaveChangesAsync();
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
            var placeExists = await _context.Places.AnyAsync(p => p.Id == like.PlaceId);

            if (!placeExists)
            {
                return NotFound("The specified PlaceId does not exist.");
            }
            
            var placeLikeFromDb = await _context.PlaceLikes
                .FirstOrDefaultAsync(pl => pl.PlaceId == like.PlaceId && pl.UserId == like.UserId);

            if (placeLikeFromDb is not null)
            {
                _context.PlaceLikes.Remove(placeLikeFromDb);
                await _context.SaveChangesAsync();
                return Ok("Dislike was Ok!");
            }

            var placeLike = new PlaceLike()
            {
                PlaceId = like.PlaceId,
                UserId = like.UserId,
            };

            await _context.PlaceLikes.AddAsync(placeLike);
            await _context.SaveChangesAsync();

            return Ok(placeLike);
        }

        [HttpPost]
        public async Task<IActionResult> Comment([FromBody] PlaceCommentDto commentDto)
        {
            var placeExists = await _context.Places.AnyAsync(p => p.Id == commentDto.PlaceId);

            if (!placeExists) return NotFound("The specified PlaceId does not exist.");
            
            var placeComment = new PlaceComment
            {
                PlaceId = commentDto.PlaceId,
                UserId = commentDto.UserId,
                Text = commentDto.Text,
            };

            await _context.PlaceComments.AddAsync(placeComment);
            await _context.SaveChangesAsync();

            return Ok(placeComment);
        }
    }
}    
            