using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Places_Service.Models;
using Places.Application.Comments.Commands;
using Places.Application.Interfaces;
using Places.Application.Likes.CreatePlaceLike;
using Places.Application.PlacePhotoComment;
using Places.Application.PlacePhotoLike.Commands;
using Places.Application.PlacePhotos.Queries;
using Places.Application.Places.Commands.CreateManyPlaces;
using Places.Application.Places.Commands.CreateOnePlace;
using Places.Application.Places.Commands.DeletePlace;
using Places.Application.Places.Commands.UpdatePlace;
using Places.Application.Places.Queries.GetPlaceByCoordinate;
using Places.Application.Places.Queries.GetPlaceById;
using Places.Application.Places.Queries.GetPlaceByName;
using Places.Application.UploadedPlacePhotos.Commands;

namespace Places_Service.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PlacesController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IGooglePlaceApi _placeApi;

        public PlacesController(IMapper mapper, IMediator mediator, IGooglePlaceApi placeApi): base(mediator)
        {
            _mapper = mapper;
            _placeApi = placeApi;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetPlacesById([FromQuery] string id,
            CancellationToken cancellationToken)
        {
            var query = new GetPlaceByIdQuery(id);

            var place = await Mediator.Send(query, cancellationToken);
            
            if (place == null) return NotFound();

            return Ok(place);
        }

        [HttpGet]
        public async Task<IActionResult> GetPlacesByName([FromQuery] string name,
            CancellationToken cancellationToken)
        {
            var query = new GetPlaceByNameQuery(name);
            
            var place = await Mediator.Send(query, cancellationToken);
            
            if (place == null) return NotFound();
            
            return Ok(place);
        }

        [HttpGet]
        public async Task<IActionResult> GetPlacesByCoordinate([FromQuery] float lat, [FromQuery] float lon,
            CancellationToken cancellationToken)
        {
            var command = new GetPlaceByCoordinateQuery(lat, lon);

            var placesNearby = await Mediator.Send(command, cancellationToken);
            
            // If places founded
            if (placesNearby != null) return Ok(placesNearby);
            
            var placeData = await _placeApi.GetPlacesByCoordinate(lat, lon, 
                cancellationToken: cancellationToken);

            var query = _mapper.Map<CreateManyPlacesCommand>(placeData);

            var vm = await Mediator.Send(query, cancellationToken);
            return Ok(vm);
        }

        [HttpPost]
        public async Task<IActionResult> LikePlace([FromBody] PlaceLikeDto placeLikeDto,
            CancellationToken cancellationToken)
        {
            var command = _mapper.Map<CreatePlaceLikeCommand>(placeLikeDto);
            var placeLike = await Mediator.Send(command, cancellationToken);
            return Ok(placeLike);
        }

        [HttpPost]
        public async Task<IActionResult> CommentPlace([FromBody] PlaceCommentDto commentDto,
            CancellationToken cancellationToken)
        {
            var command = _mapper.Map<CreatePlaceCommentCommand>(commentDto);
            var placeComment = await Mediator.Send(command, cancellationToken);
            return Ok(placeComment);
        }

        [HttpPost]
        public async Task<IActionResult> LikePlacePhoto([FromBody] PlacePhotoLikeDto photoLikeDto,
            CancellationToken cancellationToken)
        {
            var command = _mapper.Map<PlacePhotoLikeCommand>(photoLikeDto);
            var placePhotoLike = await Mediator.Send(command, cancellationToken);
            return Ok(placePhotoLike);
        }

        [HttpPost]
        public async Task<IActionResult> CommentPlacePhoto([FromBody] PlacePhotoCommentDto placePhotoCommentDto,
            CancellationToken cancellationToken)
        {
            var command = _mapper.Map<PlacePhotoCommentCommand>(placePhotoCommentDto);
            var placePhotoComment = await Mediator.Send(command, cancellationToken);
            return Ok(placePhotoComment);
        }

        [HttpPost]
        public async Task<IActionResult> AddPlace([FromBody] PlaceDto placeDto,
            CancellationToken cancellationToken)
        {
            var query = new GetPlaceByNameQuery(placeDto.Name);
            var placeFromDb = await Mediator.Send(query, cancellationToken);

            if (placeFromDb != null) return NotFound("Place already exists");

            var place = _mapper.Map<CreateOnePlaceCommand>(placeDto);
            await Mediator.Send(place, cancellationToken);

            return Ok(place);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPlacePhotos([FromQuery] GetPlacePhotoDto photoDto,
            CancellationToken cancellationToken)
        {
            var query = _mapper.Map<GetPlacePhotosQuery>(photoDto);
            var photos = await Mediator.Send(query, cancellationToken);
            return Ok(photos);
        }

        [HttpPost]
        public async Task<IActionResult> AddPlacePhoto([FromBody] UploadedPlacePhotoDto uploadPlacePhotoDto,
            CancellationToken cancellationToken)
        {
            var command = _mapper.Map<CreateUploadedPlacePhotoCommand>(uploadPlacePhotoDto);
            var uploadedPhoto = await Mediator.Send(command, cancellationToken);
            return Ok(uploadedPhoto);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePlace([FromBody] UpdatePlaceDto updatePlaceDto,
            CancellationToken cancellationToken)
        {
            var command = _mapper.Map<UpdatePlaceCommand>(updatePlaceDto);
            await Mediator.Send(command, cancellationToken);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePlace([FromQuery] string placeId,
            CancellationToken cancellationToken)
        {
            var command = new DeletePlaceCommand(placeId);
            await Mediator.Send(command, cancellationToken);
            return NoContent();
        }
    }
}    
            