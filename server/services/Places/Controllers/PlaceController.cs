using Microsoft.AspNetCore.Mvc;
using Places.Dtos;
using Places.Models;
using Places.Repository.Interface;
// using MassTransit;

namespace Places.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PlaceController : ControllerBase
    {
        private readonly IPlaceRepository _repository;
        // private readonly IBusControl _busControl;

        public PlaceController(IPlaceRepository repository)
        {
            _repository = repository;
            // _busControl = busControl;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPlaces()
        {
            var places = await _repository.GetPlacesAsync();

            if (places is null) return NotFound();
            
            // var endpoint = await _busControl.GetSendEndpoint(new Uri("rabbitmq://localhost:15672/postPlace"));
            // try
            // {
            //     await endpoint.Send<Place>(places);
            // }
            // catch (Exception ex)
            // {
            //     Console.WriteLine($"--> {ex.Message}");
            // }
            return Ok(places);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetPlaceById(int id)
        {
            var place = await _repository.GetPlaceByIdAsync(id);
            if (place == null) return NotFound();
            return Ok(place);
        }

        [HttpGet]
        public async Task<IActionResult> GetPlacesByCoordinates([FromBody] CoordinateDto coordinateDto)
        {
            var place = await _repository.GetPlaceByCoordinate(coordinateDto);

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
