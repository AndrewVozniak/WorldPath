using MediatR;

namespace Places.Application.PlacePhotos.Queries;

public class GetPlacePhotosQuery : IRequest<PlacePhotoViewModel>
{
    public string PlaceId { get; set; }
}