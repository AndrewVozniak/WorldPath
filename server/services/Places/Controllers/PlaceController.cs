using Microsoft.AspNetCore.Mvc;
using Places.Models;
using Places.Repository.Interface;

namespace Places.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceController : ControllerBase
    {
        private readonly IPlaceRepository _repository;

        public PlaceController(IPlaceRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPlaces()
        {
            var places = await _repository.GetPlacesAsync();
            return Ok(places);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetPlaceById(int id)
        {
            var place = await _repository.GetPlaceByIdAsync(id);
            if (place == null) return NotFound();
            return Ok(place);
        }

        [HttpPost("place:Place")]
        public async Task<IActionResult> AddPlace([FromBody] Place place)
        {
            await _repository.InsertPlacesAsync(place);
            await _repository.SaveAsync();

            return Created($"/GetPlacesById/{place.Id}", place);
        }

        [HttpPut("place:Place")]
        public async Task<IActionResult> UpdatePlace([FromBody] Place place)
        {
            await _repository.UpdatePlacesAsync(place);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpDelete("id:int")]
        public async Task<IActionResult> DeletePlace(int id)
        {
            await _repository.DeletePlacesAsync(id);
            await _repository.SaveAsync();

            return NoContent();
        }
    }
}
